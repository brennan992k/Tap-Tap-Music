using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtNumText : Text
{
	private readonly UIVertex[] m_TempVerts = new UIVertex[4];

	protected override void OnPopulateMesh(VertexHelper toFill)
	{
		if (base.font == null)
		{
			return;
		}
		this.m_DisableFontTextureRebuiltCallback = true;
		Vector2 size = base.rectTransform.rect.size;
		TextGenerationSettings generationSettings = base.GetGenerationSettings(size);
		base.cachedTextGenerator.Populate(this.text, generationSettings);
		Rect rect = base.rectTransform.rect;
		Vector2 textAnchorPivot = Text.GetTextAnchorPivot(base.alignment);
		Vector2 zero = Vector2.zero;
		zero.x = Mathf.Lerp(rect.xMin, rect.xMax, textAnchorPivot.x);
		zero.y = Mathf.Lerp(rect.yMin, rect.yMax, textAnchorPivot.y);
		Vector2 lhs = base.PixelAdjustPoint(zero) - zero;
		IList<UIVertex> verts = base.cachedTextGenerator.verts;
		float d = 1f / base.pixelsPerUnit;
		int num = verts.Count - 4;
		toFill.Clear();
		if (lhs != Vector2.zero)
		{
			for (int i = 0; i < num; i++)
			{
				int num2 = i & 3;
				this.m_TempVerts[num2] = verts[i];
				UIVertex[] expr_12E_cp_0 = this.m_TempVerts;
				int expr_12E_cp_1 = num2;
				expr_12E_cp_0[expr_12E_cp_1].position = expr_12E_cp_0[expr_12E_cp_1].position * d;
				UIVertex[] expr_152_cp_0_cp_0 = this.m_TempVerts;
				int expr_152_cp_0_cp_1 = num2;
				expr_152_cp_0_cp_0[expr_152_cp_0_cp_1].position.x = expr_152_cp_0_cp_0[expr_152_cp_0_cp_1].position.x + lhs.x;
				UIVertex[] expr_177_cp_0_cp_0 = this.m_TempVerts;
				int expr_177_cp_0_cp_1 = num2;
				expr_177_cp_0_cp_0[expr_177_cp_0_cp_1].position.y = expr_177_cp_0_cp_0[expr_177_cp_0_cp_1].position.y + lhs.y;
				if (num2 == 3)
				{
					toFill.AddUIVertexQuad(this.m_TempVerts);
				}
			}
		}
		else
		{
			float num3 = 0f - (float)this.text.Length * base.lineSpacing * textAnchorPivot.x;
			for (int j = 0; j < num; j++)
			{
				int num4 = j & 3;
				this.m_TempVerts[num4] = verts[j];
				UIVertex[] expr_20B_cp_0 = this.m_TempVerts;
				int expr_20B_cp_1 = num4;
				expr_20B_cp_0[expr_20B_cp_1].position = expr_20B_cp_0[expr_20B_cp_1].position * d;
				UIVertex[] expr_22A_cp_0 = this.m_TempVerts;
				int expr_22A_cp_1 = num4;
				expr_22A_cp_0[expr_22A_cp_1].position = expr_22A_cp_0[expr_22A_cp_1].position + new Vector3(num3, 0f);
				if (num4 == 3)
				{
					toFill.AddUIVertexQuad(this.m_TempVerts);
					num3 += base.lineSpacing;
				}
			}
		}
		this.m_DisableFontTextureRebuiltCallback = false;
	}
}
