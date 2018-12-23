﻿using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour {

    public BrowserView browser;

    AndroidJavaObject activity;
    AndroidJavaObject plugin;

    private void Start() {
        InitPlugin();
    }

    void InitPlugin() {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                plugin = new AndroidJavaObject(
                "com.example.matthew.plugin.VoiceBridge");
        }));

        activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            plugin.Call("StartPlugin");
        }));
    }

    public void OnVoiceResult(string recognizedText) {
        browser.ProcessQuery(recognizedText);
        Debug.Log(recognizedText);
    }

    public void GetSpeech() {
        // Calls the function from the jar file
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            plugin.Call("StartSpeaking");
        }));
    }
}
