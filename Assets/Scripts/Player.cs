using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    public event Action PickupCoin;
    public event Action Death;

    [SerializeField] private LineRenderer _pathLine;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 1, _pathDelay = 0.25f;
    [SerializeField] private Ease _movementEase = Ease.Linear;
    [SerializeField] private List<Vector2> _path = new List<Vector2>();
    private Sequence _sequence;
    private Coroutine _setPathPositions;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = _camera.ScreenToWorldPoint(Input.mousePosition);
            _path.Add(point);

            if (_path.Count <= 1) MoveOnPath();
        }
    }

    private void MoveOnPath()
    {
        _sequence.Complete();
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOMove(_path[0], Vector2.Distance(transform.position, _path[0]) / _speed))
            .SetEase(_movementEase)
            .OnPlay(() => _setPathPositions = StartCoroutine(SetPathPositions()))
            .OnComplete(() => { _path.RemoveAt(0); MoveOnPath(); StopCoroutine(_setPathPositions); });
    }

    private IEnumerator SetPathPositions()
    {
        while (_sequence.IsPlaying())
        {
            _pathLine.positionCount++;
            _pathLine.SetPosition(_pathLine.positionCount - 1, transform.position);
            yield return new WaitForSeconds(_pathDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            PickupCoin?.Invoke();
            Destroy(coin.gameObject);
        }
        if (collision.TryGetComponent(out Obstacle obstacle))
            Death?.Invoke();
    }
}