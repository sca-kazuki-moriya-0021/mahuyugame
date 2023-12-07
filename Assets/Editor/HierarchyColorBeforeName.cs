using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyColorBeforeName : MonoBehaviour
{
    static HierarchyColorBeforeName()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null)
        {
            int depth = CalculateDepth(gameObject);

            // �K�w���Ƃ̃C���f���g�̕����s�N�Z���P�ʂŌv�Z
            int indentWidth = 2; // �ʏ�̃C���f���g����14�s�N�Z���Ƃ���
            int totalIndent = indentWidth * depth;

            // �K�w�̐[���ɉ������I�t�Z�b�g��K�p�i�������p�j
            int offset = depth * 2; // �K�w�̐[����2�s�N�Z�����I�t�Z�b�g��ǉ�

            // �I�u�W�F�N�g���̑O�ɐF��`�悷�邽�߂̋�`���`
            Rect colorRect = new Rect(selectionRect.x + totalIndent - offset, selectionRect.y, 3, selectionRect.height);

            // �F������
            Color color = Color.HSVToRGB((depth * 0.1f) % 1.0f, 0.6f, 0.8f);

            // �I�u�W�F�N�g���̑O�ɐF��`��
            EditorGUI.DrawRect(colorRect, color);
        }
    }

    private static int CalculateDepth(GameObject obj)
    {
        int depth = 0;
        while (obj.transform.parent != null)
        {
            depth++;
            obj = obj.transform.parent.gameObject;
        }
        return depth;
    }
}
