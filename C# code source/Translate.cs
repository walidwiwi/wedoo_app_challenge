﻿// We need this for parsing the JSON, unless you use an alternative.
// You will need SimpleJSON if you don't use alternatives.
// It can be gotten hither. http://wiki.unity3d.com/index.php/SimpleJSON
using SimpleJSON;
using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Translate : MonoBehaviour
{
	private Text text; 
    private void Start()
    {
		text = GetComponent<Text>();
		WedooTranslate.instance.ChangeTranslate += trans ; 
	}
    private void OnDestroy()
    {
		WedooTranslate.instance.ChangeTranslate -= trans; 
    }
    public void trans()
    {
		if(text == null)
        {
			print(gameObject.name);
			return; 
        }
		StartCoroutine(Process(WedooTranslate.instance.LanguageText.text, text.text));
 
    }

	// We have use googles own api built into google Translator.
	public IEnumerator Process(string targetLang, string sourceText)
	{
		print("start getting trans"); 
		// We use Auto by default to determine if google can figure it out.. sometimes it can't.
		string sourceLang = "auto";
		// Construct the url using our variables and googles api.
		string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
			+ sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

		// Put together our unity bits for the web call.
		WWW www = new WWW(url);
		// Now we actually make the call and wait for it to finish.
		yield return www;

		// Check to see if it's done.
		if (www.isDone)
		{
			// Check to see if we don't have any errors.
			if (string.IsNullOrEmpty(www.error))
			{
				// Parse the response using JSON.
				var N = JSONNode.Parse(www.text);
				// Dig through and take apart the text to get to the good stuff.
				text.text = N[0][0][0];
				// This is purely for debugging in the Editor to see if it's the word you wanted.

			}
		}
	}

	// Exactly the same as above but allow the user to change from Auto, for when google get's all Jerk Butt-y
	public IEnumerator Process(string sourceLang, string targetLang, string sourceText)
	{
		string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
			+ sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

		WWW www = new WWW(url);
		yield return www;

		if (www.isDone)
		{
			if (string.IsNullOrEmpty(www.error))
			{
				var N = JSONNode.Parse(www.text);
				text.text = N[0][0][0];

			}
		}
	}
}