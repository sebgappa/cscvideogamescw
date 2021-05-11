using System.IO;
using UnityEngine;

public class ReplayService : MonoBehaviour
{
    private Player[] _playerComponents;

    private MemoryStream memoryStream = null;
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;

    private bool recordingInitialized = false;
    private bool recording = false;
    private bool replaying = false;

    private int currentRecordingFrames = 0;
    private int maxRecordingFrames = 1800;

    private int replayFrameLength = 2;
    private int replayFrameTimer = 0;

    public void Start()
    {
        _playerComponents = FindObjectsOfType<Player>(); 
        StartRecording();
    }

    public void FixedUpdate()
    {
        if (recording)
        {
            UpdateRecording();
        }
        else if (replaying)
        {
            UpdateReplaying();
        }
    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;
    }

    public void StartRecording()
    {
        if (!recordingInitialized)
        {
            InitializeRecording();
        }
        else
        {
            memoryStream.SetLength(0);
        }
        ResetReplayFrame();

        StartReplayFrameTimer();
        recording = true;
    }

    private void UpdateRecording()
    {
        if (currentRecordingFrames > maxRecordingFrames)
        {
            StopRecording();
            currentRecordingFrames = 0;
            return;
        }

        if (replayFrameTimer == 0)
        {
            SaveComponents();
            ResetReplayFrameTimer();
        }
        --replayFrameTimer;
        ++currentRecordingFrames;
    }

    public void StopRecording()
    {
        recording = false;
    }

    private void ResetReplayFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    public void StartReplaying()
    {
        ReplayPlayerCommands();
        ResetReplayFrame();
        StartReplayFrameTimer();
        replaying = true;
    }

    private void ReplayPlayerCommands()
    {
        for(int i = 0; i < _playerComponents.Length; i++)
        {
            var commandDispatcer = _playerComponents[i].GetComponent<CommandDispatcher>();
            StartCoroutine(commandDispatcer.ReplayCommands());
        }
    }

    private void UpdateReplaying()
    {
        if (memoryStream.Position >= memoryStream.Length)
        {
            StopReplaying();
            return;
        }

        if (replayFrameTimer == 0)
        {
            LoadComponents();
            ResetReplayFrameTimer();
        }
        --replayFrameTimer;
    }

    private void StopReplaying()
    {
        replaying = false;
    }

    private void ResetReplayFrameTimer()
    {
        replayFrameTimer = replayFrameLength;
    }

    private void StartReplayFrameTimer()
    {
        replayFrameTimer = 0;
    }

    private void SaveComponents()
    {
        for (int i = 0; i < _playerComponents.Length; i++)
        {
            SaveTransform(_playerComponents[i].GetComponent<Transform>());
            SaveVelocity(_playerComponents[i].GetComponent<Rigidbody2D>());
            SaveOrientation(_playerComponents[i].GetComponent<SpriteRenderer>());
        }
    }

    private void SaveTransform(Transform transform)
    {
        binaryWriter.Write(transform.localPosition.x);
        binaryWriter.Write(transform.localPosition.y);
        binaryWriter.Write(transform.localPosition.z);
    }

    private void SaveVelocity(Rigidbody2D rigidbody2D)
    {
        binaryWriter.Write(rigidbody2D.velocity.x);
        binaryWriter.Write(rigidbody2D.velocity.y);
    }

    private void SaveOrientation(SpriteRenderer spriteRenderer)
    {
        binaryWriter.Write(spriteRenderer.flipX);
    }

    private void LoadComponents()
    {
        for (int i = 0; i < _playerComponents.Length; i++)
        {
            LoadPlayerReplay(
                _playerComponents[i].GetComponent<Transform>(),
                _playerComponents[i].GetComponent<Rigidbody2D>(),
                _playerComponents[i].GetComponent<Animator>(),
                _playerComponents[i].GetComponent<SpriteRenderer>());
        }
    }

    private void LoadPlayerReplay(Transform transform, Rigidbody2D rigidbody2D, Animator animator, SpriteRenderer spriteRenderer)
    {
        float x = binaryReader.ReadSingle();
        float y = binaryReader.ReadSingle();
        float z = binaryReader.ReadSingle();

        transform.localPosition = new Vector3(x, y, z);

        float xVelocity = binaryReader.ReadSingle();
        float yVelocity = binaryReader.ReadSingle();

        bool orientation = binaryReader.ReadBoolean();
        spriteRenderer.flipX = orientation;

        var velocity = rigidbody2D.velocity;
        velocity.x = xVelocity;
        velocity.y = yVelocity;
        rigidbody2D.velocity = velocity;

        if (velocity.x != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(velocity.y));
        }
    }
}
