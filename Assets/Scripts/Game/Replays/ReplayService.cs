using System.IO;
using UnityEngine;

public class ReplayService : MonoBehaviour
{
    private Player[] _playerComponents;

    private MemoryStream _stream = null;
    private BinaryWriter _writer = null;
    private BinaryReader _reader = null;

    private bool initialized = false;
    private bool recording = false;
    private bool replaying = false;

    private int _currentRecordingFrames = 0;
    private int _maximumRecordingFrames = 1800;

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

    private void Initialize()
    {
        _stream = new MemoryStream();
        _writer = new BinaryWriter(_stream);
        _reader = new BinaryReader(_stream);
        initialized = true;
    }

    public void StartRecording()
    {
        if (!initialized)
        {
            Initialize();
        }
        else
        {
            _stream.SetLength(0);
        }
        ResetReplayFrame();

        StartReplayFrameTimer();
        recording = true;
    }

    private void UpdateRecording()
    {
        if (_currentRecordingFrames > _maximumRecordingFrames)
        {
            StopRecording();
            _currentRecordingFrames = 0;
            return;
        }

        if (replayFrameTimer == 0)
        {
            SaveComponents();
            ResetReplayFrameTimer();
        }
        --replayFrameTimer;
        ++_currentRecordingFrames;
    }

    public void StopRecording()
    {
        recording = false;
    }

    private void ResetReplayFrame()
    {
        _stream.Seek(0, SeekOrigin.Begin);
        _writer.Seek(0, SeekOrigin.Begin);
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
        if (_stream.Position >= _stream.Length)
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
        _writer.Write(transform.localPosition.x);
        _writer.Write(transform.localPosition.y);
        _writer.Write(transform.localPosition.z);
    }

    private void SaveVelocity(Rigidbody2D rigidbody2D)
    {
        _writer.Write(rigidbody2D.velocity.x);
        _writer.Write(rigidbody2D.velocity.y);
    }

    private void SaveOrientation(SpriteRenderer spriteRenderer)
    {
        _writer.Write(spriteRenderer.flipX);
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
        float x = _reader.ReadSingle();
        float y = _reader.ReadSingle();
        float z = _reader.ReadSingle();

        transform.localPosition = new Vector3(x, y, z);

        float xVelocity = _reader.ReadSingle();
        float yVelocity = _reader.ReadSingle();

        bool orientation = _reader.ReadBoolean();
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
