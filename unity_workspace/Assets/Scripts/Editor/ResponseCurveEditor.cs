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

			float width = EditorGUIUtility.currentViewWidth * 0.75f;
			Rect layoutRectangle = GUILayoutUtility.GetRect(width, width);

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
				// axis colour
				GL.Color(Color.white);

				float xMin = 10.0f;
				float xMax = layoutRectangle.width - 10.0f;
				float yMin = 10.0f;
				float yMax = layoutRectangle.height - 10.0f;

				// x axis
				GL.Vertex3(xMin, yMin, 0.0f);
				GL.Vertex3(xMin, yMax, 0.0f);

				// y axis
				GL.Vertex3(xMin, yMax, 0.0f);
				GL.Vertex3(xMax, yMax, 0.0f);

				// graph colour
				GL.Color(Color.green);

				if (m_responseCurve.m_edges.Count == 0)
				{
					GL.Vertex3(xMin, yMax - 1.0f, 0.0f);
					GL.Vertex3(xMax, yMax - 1.0f, 0.0f);
				}
				else if (m_responseCurve.m_edges.Count == 1)
				{
					float halfY = yMin + ((yMax - yMin) * 0.5f);
					GL.Vertex3(xMin, halfY, 0.0f);
					GL.Vertex3(xMax, halfY, 0.0f);
				}
				else
				{
					float xStep = (xMax - xMin) / (m_responseCurve.m_edges.Count - 1);
					float currentX = xMin;
					float yDiff = yMax - yMin;

					float maxEdgeValue = 0.0f;
					m_responseCurve.m_edges.ForEach(edge => maxEdgeValue = edge > maxEdgeValue ? edge : maxEdgeValue);

					for (int i = 1; i < m_responseCurve.m_edges.Count; ++i)
					{
						GL.Vertex3(currentX, yMax - ((m_responseCurve.m_edges[i - 1] / maxEdgeValue) * yDiff), 0.0f);
						currentX += xStep;
						GL.Vertex3(currentX, yMax - ((m_responseCurve.m_edges[i] / maxEdgeValue) * yDiff), 0.0f);
					}
				}

				// end drawing lines
				GL.End();

				// end
				GL.PopMatrix();
				GUI.EndClip();
			}
			GUILayout.EndHorizontal();
		}

		serializedObject.ApplyModifiedProperties();
	}

}