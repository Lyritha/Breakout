using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDefinition))]
public class LevelDefinitionEditor : Editor
{
    private const float BoxSize = 33f;
    private const float BoxSpacing = 3f;

    public override void OnInspectorGUI()
    {
        LevelDefinition level = (LevelDefinition)target;
        level.rows ??= new LevelRow[0];

        int widest = GetWidest(level);

        EditorGUILayout.Space(10);

        for (int i = 0; i < level.rows.Length; i++)
        {
            DrawRow(level.rows[i], widest);
            EditorGUILayout.Space(5);
        }

        DrawAddRemove(level);

        if (GUI.changed)
            EditorUtility.SetDirty(level);
    }

    int GetWidest(LevelDefinition level)
    {
        int widest = 0;

        foreach (LevelRow r in level.rows)
            if (r?.slots != null) widest = Mathf.Max(widest, r.slots.Length);

        return widest;
    }

    void DrawRow(LevelRow row, int widest)
    {
        EditorGUILayout.BeginHorizontal();

        row.color = EditorGUILayout.ColorField(row.color, GUILayout.Width(60));
        row.score = EditorGUILayout.IntField(new GUIContent("", "Score for this row"), row.score, GUILayout.Width(50));

        row.slots ??= new bool[1];

        DrawRowButtons(row);
        DrawPadding(row, widest);

        for (int s = 0; s < row.slots.Length; s++) DrawSlot(row, s);

        EditorGUILayout.EndHorizontal();
    }

    void DrawRowButtons(LevelRow row)
    {
        if (GUILayout.Button("+", GUILayout.Width(25))) ArrayUtility.Add(ref row.slots, false);

        if (GUILayout.Button("-", GUILayout.Width(25)))
            if (row.slots.Length > 1) ArrayUtility.RemoveAt(ref row.slots, row.slots.Length - 1);

        if (GUILayout.Button("Fill", GUILayout.Width(40)))
            for (int i = 0; i < row.slots.Length; i++) row.slots[i] = true;

        if (GUILayout.Button("Empty", GUILayout.Width(50)))
            for (int i = 0; i < row.slots.Length; i++) row.slots[i] = false;
    }

    void DrawPadding(LevelRow row, int widest)
    {
        int missing = widest - row.slots.Length;
        float pad = missing * (BoxSize + BoxSpacing) * 0.5f;
        GUILayout.Space(pad);
    }

    void DrawSlot(LevelRow row, int index)
    {
        float h = BoxSize / 2f;
        float rowH = EditorGUIUtility.singleLineHeight;
        float offset = (rowH - h) * 0.5f;

        Rect r = GUILayoutUtility.GetRect(BoxSize, h, GUILayout.Width(BoxSize), GUILayout.Height(h));
        r.y += offset;

        DrawSlotBackground(row, index, r);
        DrawSlotToggle(row, index, r);

        GUILayout.Space(BoxSpacing);
    }

    void DrawSlotBackground(LevelRow row, int index, Rect r)
    {
        if (row.slots[index])
        {
            // Full‑size colored block
            EditorGUI.DrawRect(r, row.color);
            return;
        }

        // Smaller centered grey block
        float shrink = 0.5f;
        Rect inner = new(
            r.x + (r.width * (1f - shrink) * 0.5f),
            r.y + (r.height * (1f - shrink) * 0.5f),
            r.width * shrink,
            r.height * shrink
        );

        EditorGUI.DrawRect(inner, new Color(0.4f, 0.4f, 0.4f));
    }


    void DrawSlotToggle(LevelRow row, int index, Rect r)
    {
        row.slots[index] = GUI.Toggle(r, row.slots[index], GUIContent.none, GUIStyle.none);
    }

    void DrawAddRemove(LevelDefinition level)
    {
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Add Row"))
            ArrayUtility.Add(ref level.rows, new LevelRow { slots = new bool[5] });

        if (GUILayout.Button("Remove Last Row"))
            if (level.rows.Length > 0)
                ArrayUtility.RemoveAt(ref level.rows, level.rows.Length - 1);
    }
}
