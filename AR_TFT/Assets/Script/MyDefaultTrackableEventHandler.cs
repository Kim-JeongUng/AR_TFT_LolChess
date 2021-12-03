using UnityEngine;
using Vuforia;

public class MyDefaultTrackableEventHandler : DefaultTrackableEventHandler
{
    public bool isAttach= false;
    protected override void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            { 
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);
            var audioComponents = mTrackableBehaviour.GetComponentsInChildren<AudioSource>(true);
            var animationComponents = mTrackableBehaviour.GetComponentsInChildren<Animator>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;

            // Enable audios:
            foreach (var component in audioComponents)
                component.enabled = true;

            // Enable animations:
            foreach (var component in animationComponents)
                component.enabled = true;
            } 
            isAttach = true;
        }
    }
    protected override void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);
            var audioComponents = mTrackableBehaviour.GetComponentsInChildren<AudioSource>(true);
            var animationComponents = mTrackableBehaviour.GetComponentsInChildren<Animator>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
/*
            // Disable audios:
            foreach (var component in audioComponents)
                component.enabled = false;*/

            // Disable animations:
            foreach (var component in animationComponents)
                component.enabled = false;

            isAttach = false;
        }
    }
}