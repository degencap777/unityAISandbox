using UnityEditor;

[CustomPropertyDrawer(typeof(String_Int_Dictionary))]
public class String_Int_DictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<string, int> { }

[CustomPropertyDrawer(typeof(PerceptionEventType_Float_Dictionary))]
public class PerceptionEventType_Float_DictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<PerceptionEventType, float> { }