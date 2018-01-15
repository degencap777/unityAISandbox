using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResponseCurve))]
public class ResponseCurveEditor : Editor
{

	private ResponseCurve m_responseCurve = null;
	private Material m_material = null;

	private int m_removeIndex = -1;
	private int m_shiftUpIndex = -1;
	private int m_shiftDownIndex = -1;
	
	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_responseCurve = target as ResponseCurve;
		m_material = new Material(Shader.Find("Hidden/Internal-Colored"));
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		m_removeIndex = -1;
		m_shiftUpIndex = -1;
		m_shiftDownIndex = -1;

		if (m_responseCurve != null)
		{
			EditorGUILayout.LabelField("Edges");
			for (int i = 0; i < m_responseCurve.m_edges.Count; ++i)
			{
				EditorGUILayout.BeginHorizontal();

				m_responseCurve.m_edges[i] = EditorGUILayout.FloatField(m_responseCurve.m_edges[i]);

				// remove element
				if (GUILayout.Button("Remove"))
				{
					m_removeIndex = i;
				}

				// shift up
				EditorGUI.BeginDisabledGroup(i == 0);
				if (GUILayout.Button("Move up"))
				{
					m_shiftUpIndex = i;
				}
				EditorGUI.EndDisabledGroup();

				// shift down
				EditorGUI.BeginDisabledGroup(i == (m_responseCurve.m_edges.Count - 1));
				if (GUILayout.Button("Move down"))
				{
					m_shiftDownIndex = i;
				}
				EditorGUI.EndDisabledGroup();

				EditorGUILayout.EndHorizontal();
			}

			// modify list if any buttons pressed (after iterating list)
			if (m_removeIndex > -1)
			{
				m_responseCurve.m_edges.RemoveAt(m_removeIndex);
			}
			else if (m_shiftUpIndex > -1)
			{
				float temp = m_responseCurve.m_edges[m_shiftUpIndex];
				m_responseCurve.m_edges[m_shiftUpIndex] = m_responseCurve.m_edges[m_shiftUpIndex - 1];
				m_responseCurve.m_edges[m_shiftUpIndex - 1] = temp;
			}
			else if (m_shiftDownIndex > -1)
			{
				float temp = m_responseCurve.m_edges[m_shiftDownIndex];
				m_responseCurve.m_edges[m_shiftDownIndex] = m_responseCurve.m_edges[m_shiftDownIndex + 1];
				m_responseCurve.m_edges[m_shiftDownIndex + 1] = temp;
			}

			// add edge
			if (GUILayout.Button("+"))
			{
				m_responseCurve.m_edges.Add(0.0f);
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Curve");

			// curve container rectangle
			GUILayout.BeginHorizontal(EditorStyles.helpBox);
			Rect layoutRectangle = GUILayoutUtility.GetRect(100.0f, 140.0f);

			if (Event.current.type == EventType.Repaint)
			{
				// begin
				GUI.BeginClip(layoutRectangle);
				GL.PushMatrix();
				GL.Clear(true, false, Color.black);
				m_material.SetPass(0);

				// background
				GL.Begin(GL.QUADS);
				GL.Color(Color.black);
				GL.Vertex3(0, 0, 0);
				GL.Vertex3(layoutRectangle.width, 0, 0);
				GL.Vertex3(layoutRectangle.width, layoutRectangle.height, 0);
				GL.Vertex3(0, layoutRectangle.height, 0);
				GL.End();

				// start drawing lines
				GL.Begin(GL.LINES);
				
				// #SteveD >>> cache inner rect values

				// x axis
				// #SteveD >>> fix this
				GL.Vertex3(layoutRectangle.x, layoutRectangle.y - layoutRectangle.height, 0);
				GL.Vertex3(layoutRectangle.x + (layoutRectangle.width - 50), layoutRectangle.y - layoutRectangle.height, 0);

				// y axis
				// ...
				
				// #SteveD >>> todo >>> draw graph
				// - if 0 edges, draw solid line at 0.0f
				// - if 1 edge, draw solid line at this edge value
				// - draw lines connecting edges

				// end drawing lines
				GL.End();

				// end
				GL.PopMatrix();
				GUI.EndClip();
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

}