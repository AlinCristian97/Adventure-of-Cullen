using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces.ObserverPattern;
using UnityEngine;

public class GroundCheck : MonoBehaviour, ISubject
{
    [SerializeField] private Character _character;
    private List<IObserver> _observers = new List<IObserver>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _character.IsGrounded = true;
        NotifyObservers();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _character.IsGrounded = false;
        NotifyObservers();
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        if (_observers.Count < 1) return;
        foreach (IObserver observer in _observers)
        {
            observer.GetNotified();
        }
    }
}