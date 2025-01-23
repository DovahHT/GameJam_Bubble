using System;

namespace EventDomain.Core
{
    public enum EEventType
    {
        OnBubbleSpawn,
        OnBubbleDestroy,
        OnBubblePass,
    }

    public static class EventExtension
    {
        //public static void ListenToEvent(this IEvent eventController, EEventType eventType, Action action)
        public static void ListenToEvent(this EventSystem eventController, EEventType eventType, Action action)
        {
            eventController.ListenToEvent((int)eventType, action);
        }

        public static void ListenToEvent<T>(this EventSystem eventController, EEventType eventType, Action<T> action)
        {
            eventController.ListenToEvent((int)eventType, action);
        }

        public static void ListenToEvent<T, U>(this EventSystem eventController, EEventType eventType, Action<T, U> action)
        {
            eventController.ListenToEvent((int)eventType, action);
        }

        public static void ListenToEvent<T, U, V>(this EventSystem eventController, EEventType eventType,
            Action<T, U, V> action)
        {
            eventController.ListenToEvent((int)eventType, action);
        }

        public static void RemoveEventListener(this EventSystem eventController, EEventType eventType, Action action)
        {
            eventController.RemoveEventListener((int)eventType, action);
        }

        public static void RemoveEventListener<T>(this EventSystem eventController, EEventType eventType, Action<T> action)
        {
            eventController.RemoveEventListener((int)eventType, action);
        }

        public static void RemoveEventListener<T, U>(this EventSystem eventController, EEventType eventType,
            Action<T, U> action)
        {
            eventController.RemoveEventListener((int)eventType, action);
        }

        public static void RemoveEventListener<T, U, V>(this EventSystem eventController, EEventType eventType,
            Action<T, U, V> action)
        {
            eventController.RemoveEventListener((int)eventType, action);
        }

        public static void BroadcastEvent(this EventSystem eventController, EEventType eventType)
        {
            eventController.BroadcastEvent((int)eventType);
        }

        public static void BroadcastEvent<T>(this EventSystem eventController, EEventType eventType, T inputValue)
        {
            eventController.BroadcastEvent((int)eventType, inputValue);
        }

        public static void BroadcastEvent<T, U>(this EventSystem eventController, EEventType eventType, T inputValue,
            U inputValue2)
        {
            eventController.BroadcastEvent((int)eventType, inputValue, inputValue2);
        }

        public static void BroadcastEvent<T, U, V>(this EventSystem eventController, EEventType eventType, T inputValue,
            U inputValue2, V inputValue3)
        {
            eventController.BroadcastEvent((int)eventType, inputValue, inputValue2, inputValue3);
        }
    }
}