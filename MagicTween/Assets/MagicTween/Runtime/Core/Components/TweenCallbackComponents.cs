using System;
using Unity.Entities;
using UnityEngine;

namespace MagicTween.Core.Components
{
    public sealed class TweenPropertyAccessor<T> : IComponentData
    {
        public TweenGetter<T> getter;
        public TweenSetter<T> setter;

        public TweenPropertyAccessor() { }

        // Implement pooling using Dispose of Managed Component.
        // This is not the expected use of Dispose, but it works.
        public void Dispose()
        {
            TweenPropertyAccessorPool<T>.Return(this);
        }
    }

    // Since it is difficult to use generics due to ECS restrictions,
    // use UnsafeUtility.As to force assignment of delegates when creating components.
    // So the type of the target and the type of the TweenGetter/TweenSetter argument must match absolutely,
    // otherwise undefined behavior will result.
    public sealed class TweenPropertyAccessorUnsafe<T> : IComponentData, IDisposable
    {
        [HideInInspector] public object target;
        [HideInInspector] public TweenGetter<object, T> getter;
        [HideInInspector] public TweenSetter<object, T> setter;

        public TweenPropertyAccessorUnsafe() { }

        public void Dispose()
        {
            TweenPropertyAccessorUnsafePool<T>.Return(this);
        }
    }

    public sealed class TweenCallbackActions : IComponentData
    {
        public Action onStart;
        public Action onPlay;
        public Action onPause;
        public Action onUpdate;
        public Action onStepComplete;
        public Action onComplete;
        public Action onKill;

        // internal callback
        public Action onRewind;
    }
}