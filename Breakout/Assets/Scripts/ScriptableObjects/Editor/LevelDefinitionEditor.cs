using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelDefinition))]
public class LevelDefinitionEditor : Editor
{
    private const float BoxSize = 33f;
    private const float BoxSpacing = 3f;

    private ReorderableList rowList;

    private void OnEnable()
    {
        SerializedProperty rowsProp = serializedObject.FindProperty("rows");

        rowList = new ReorderableList(
            serializedObject,
            rowsProp,
            draggable: true,
            displayHeader: true,
            displayAddButton: true,
            displayRemoveButton: true
        );

        rowList.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Rows");
        };

        rowList.elementHeightCallback = index =>
        {
            return EditorGUIUtility.singleLineHeight * 2.2f;
        };

        rowList.drawElementCallback = (rect, index, active, focused) =>
        {
            LevelDefinition level = (LevelDefinition)target;
            LevelRow row = level.rows[index];

            int widest = GetWidest(level);

            rect.y += 2;
            DrawRow(rect, row, widest);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        LevelDefinition level = (LevelDefinition)target;
        level.rows ??= new LevelRow[0];

        level.levelType = (LevelType)EditorGUILayout.EnumPopup("Level Type", level.levelType);

        EditorGUILayout.BeginHorizontal();
        level.levelColor = EditorGUILayout.ColorField(level.levelColor, GUILayout.Width(60));
        level.levelName = EditorGUILayout.TextField(level.levelName);
        EditorGUILayout.EndHorizontal();

        if (level.levelType != LevelType.Boss)
        {
            EditorGUILayout.Space(10);
            rowList.DoLayoutList();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed) EditorUtility.SetDirty(level);
    }

    // ---------------------------------------------------------
    // RECT-BASED DRAWING (NO GUILayout inside list elements)
    // ---------------------------------------------------------

    int GetWidest(LevelDefinition level)
    {
        int widest = 0;

        foreach (LevelRow r in level.rows)
            if (r?.slots != null)
                widest = Mathf.Max(widest, r.slots.Length);

        return widest;
    }

    void DrawRow(Rect rect, LevelRow row, int widest)
    {
        float x = rect.x;
        float y = rect.y;
        float h = EditorGUIUtility.singleLineHeight;

        // Color
        Rect colorRect = new Rect(x, y, 60, h);
        row.color = EditorGUI.ColorField(colorRect, row.color);
        x += 65;

        // Score
        Rect scoreRect = new Rect(x, y, 50, h);
        row.score = EditorGUI.IntField(scoreRect, row.score);
        x += 55;

        // Buttons
        x = DrawRowButtons(rect, row, x, y);

        // Padding
        x += GetPadding(row, widest);

        // Slots
        for (int i = 0; i < row.slots.Length; i++)
        {
            Rect slotRect = new Rect(x, y, BoxSize, BoxSize / 2f);
            DrawSlot(row, i, slotRect);
            x += BoxSize + BoxSpacing;
        }

        Rect colRect = new Rect(x, y, 50, h);
        EditorGUI.LabelField(colRect, row.slots.Length.ToString());
    }

    float GetPadding(LevelRow row, int widest)
    {
        int missing = widest - row.slots.Length;
        return missing * (BoxSize + BoxSpacing) * 0.5f;
    }

    float DrawRowButtons(Rect rect, LevelRow row, float x, float y)
    {
        float h = EditorGUIUtility.singleLineHeight;

        if (GUI.Button(new Rect(x, y, 25, h), "+"))
            ArrayUtility.Add(ref row.slots, false);
        x += 30;

        if (GUI.Button(new Rect(x, y, 25, h), "-"))
            if (row.slots.Length > 1)
                ArrayUtility.RemoveAt(ref row.slots, row.slots.Length - 1);
        x += 30;

        if (GUI.Button(new Rect(x, y, 40, h), "Fill"))
            for (int i = 0; i < row.slots.Length; i++)
                row.slots[i] = true;
        x += 45;

        if (GUI.Button(new Rect(x, y, 50, h), "Empty"))
            for (int i = 0; i < row.slots.Length; i++)
                row.slots[i] = false;
        x += 55;

        return x;
    }

    void DrawSlot(LevelRow row, int index, Rect r)
    {
        DrawSlotBackground(row, index, r);
        row.slots[index] = GUI.Toggle(r, row.slots[index], GUIContent.none, GUIStyle.none);
    }

    void DrawSlotBackground(LevelRow row, int index, Rect r)
    {
        if (row.slots[index])
        {
            EditorGUI.DrawRect(r, row.color);
            return;
        }

        float shrink = 0.5f;
        Rect inner = new Rect(
            r.x + (r.width * (1f - shrink) * 0.5f),
            r.y + (r.height * (1f - shrink) * 0.5f),
            r.width * shrink,
            r.height * shrink
        );

        EditorGUI.DrawRect(inner, new Color(0.4f, 0.4f, 0.4f));
    }
}
