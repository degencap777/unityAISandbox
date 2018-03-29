using UnityEditor;


[CustomPropertyDrawer(typeof(Int_Float_Dictionary))]
[CanEditMultipleObjects]
public class Int_Float_DictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<int, float> { }
