using System;
using UnityEngine;

namespace AppAdvisory.TunnelAndTwist
{
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class RoundedCube : MonoBehaviour
	{
		public int xSize;

		public int ySize;

		public int zSize;

		public int roundness;

		private Mesh mesh;

		private Vector3[] vertices;

		private Vector3[] normals;

		private Color32[] cubeUV;

		private void Awake()
		{
			this.Generate();
		}

		private void Generate()
		{
			base.GetComponent<MeshFilter>().mesh = (this.mesh = new Mesh());
			this.mesh.name = "Procedural Cube";
			this.CreateVertices();
			this.CreateTriangles();
			this.CreateColliders();
		}

		private void CreateVertices()
		{
			int num = 8;
			int num2 = (this.xSize + this.ySize + this.zSize - 3) * 4;
			int num3 = ((this.xSize - 1) * (this.ySize - 1) + (this.xSize - 1) * (this.zSize - 1) + (this.ySize - 1) * (this.zSize - 1)) * 2;
			this.vertices = new Vector3[num + num2 + num3];
			this.normals = new Vector3[this.vertices.Length];
			this.cubeUV = new Color32[this.vertices.Length];
			int num4 = 0;
			for (int i = 0; i <= this.ySize; i++)
			{
				for (int j = 0; j <= this.xSize; j++)
				{
					this.SetVertex(num4++, j, i, 0);
				}
				for (int k = 1; k <= this.zSize; k++)
				{
					this.SetVertex(num4++, this.xSize, i, k);
				}
				for (int l = this.xSize - 1; l >= 0; l--)
				{
					this.SetVertex(num4++, l, i, this.zSize);
				}
				for (int m = this.zSize - 1; m > 0; m--)
				{
					this.SetVertex(num4++, 0, i, m);
				}
			}
			for (int n = 1; n < this.zSize; n++)
			{
				for (int num5 = 1; num5 < this.xSize; num5++)
				{
					this.SetVertex(num4++, num5, this.ySize, n);
				}
			}
			for (int num6 = 1; num6 < this.zSize; num6++)
			{
				for (int num7 = 1; num7 < this.xSize; num7++)
				{
					this.SetVertex(num4++, num7, 0, num6);
				}
			}
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
			this.mesh.colors32 = this.cubeUV;
		}

		private void SetVertex(int i, int x, int y, int z)
		{
			Vector3 vector = this.vertices[i] = new Vector3((float)x, (float)y, (float)z);
			if (x < this.roundness)
			{
				vector.x = (float)this.roundness;
			}
			else if (x > this.xSize - this.roundness)
			{
				vector.x = (float)(this.xSize - this.roundness);
			}
			if (y < this.roundness)
			{
				vector.y = (float)this.roundness;
			}
			else if (y > this.ySize - this.roundness)
			{
				vector.y = (float)(this.ySize - this.roundness);
			}
			if (z < this.roundness)
			{
				vector.z = (float)this.roundness;
			}
			else if (z > this.zSize - this.roundness)
			{
				vector.z = (float)(this.zSize - this.roundness);
			}
			this.normals[i] = (this.vertices[i] - vector).normalized;
			this.vertices[i] = vector + this.normals[i] * (float)this.roundness;
			this.cubeUV[i] = new Color32((byte)x, (byte)y, (byte)z, 0);
		}

		private void CreateTriangles()
		{
			int[] triangles = new int[this.xSize * this.ySize * 12];
			int[] triangles2 = new int[this.ySize * this.zSize * 12];
			int[] triangles3 = new int[this.xSize * this.zSize * 12];
			int num = (this.xSize + this.zSize) * 2;
			int i = 0;
			int i2 = 0;
			int t = 0;
			int num2 = 0;
			int j = 0;
			while (j < this.ySize)
			{
				int k = 0;
				while (k < this.xSize)
				{
					i = RoundedCube.SetQuad(triangles, i, num2, num2 + 1, num2 + num, num2 + num + 1);
					k++;
					num2++;
				}
				int l = 0;
				while (l < this.zSize)
				{
					i2 = RoundedCube.SetQuad(triangles2, i2, num2, num2 + 1, num2 + num, num2 + num + 1);
					l++;
					num2++;
				}
				int m = 0;
				while (m < this.xSize)
				{
					i = RoundedCube.SetQuad(triangles, i, num2, num2 + 1, num2 + num, num2 + num + 1);
					m++;
					num2++;
				}
				int n = 0;
				while (n < this.zSize - 1)
				{
					i2 = RoundedCube.SetQuad(triangles2, i2, num2, num2 + 1, num2 + num, num2 + num + 1);
					n++;
					num2++;
				}
				i2 = RoundedCube.SetQuad(triangles2, i2, num2, num2 - num + 1, num2 + num, num2 + 1);
				j++;
				num2++;
			}
			t = this.CreateTopFace(triangles3, t, num);
			t = this.CreateBottomFace(triangles3, t, num);
			this.mesh.subMeshCount = 3;
			this.mesh.SetTriangles(triangles, 0);
			this.mesh.SetTriangles(triangles2, 1);
			this.mesh.SetTriangles(triangles3, 2);
		}

		private int CreateTopFace(int[] triangles, int t, int ring)
		{
			int num = ring * this.ySize;
			int i = 0;
			while (i < this.xSize - 1)
			{
				t = RoundedCube.SetQuad(triangles, t, num, num + 1, num + ring - 1, num + ring);
				i++;
				num++;
			}
			t = RoundedCube.SetQuad(triangles, t, num, num + 1, num + ring - 1, num + 2);
			int num2 = ring * (this.ySize + 1) - 1;
			int num3 = num2 + 1;
			int num4 = num + 2;
			int j = 1;
			while (j < this.zSize - 1)
			{
				t = RoundedCube.SetQuad(triangles, t, num2, num3, num2 - 1, num3 + this.xSize - 1);
				int k = 1;
				while (k < this.xSize - 1)
				{
					t = RoundedCube.SetQuad(triangles, t, num3, num3 + 1, num3 + this.xSize - 1, num3 + this.xSize);
					k++;
					num3++;
				}
				t = RoundedCube.SetQuad(triangles, t, num3, num4, num3 + this.xSize - 1, num4 + 1);
				j++;
				num2--;
				num3++;
				num4++;
			}
			int num5 = num2 - 2;
			t = RoundedCube.SetQuad(triangles, t, num2, num3, num5 + 1, num5);
			int l = 1;
			while (l < this.xSize - 1)
			{
				t = RoundedCube.SetQuad(triangles, t, num3, num3 + 1, num5, num5 - 1);
				l++;
				num5--;
				num3++;
			}
			t = RoundedCube.SetQuad(triangles, t, num3, num5 - 2, num5, num5 - 1);
			return t;
		}

		private int CreateBottomFace(int[] triangles, int t, int ring)
		{
			int num = 1;
			int num2 = this.vertices.Length - (this.xSize - 1) * (this.zSize - 1);
			t = RoundedCube.SetQuad(triangles, t, ring - 1, num2, 0, 1);
			int i = 1;
			while (i < this.xSize - 1)
			{
				t = RoundedCube.SetQuad(triangles, t, num2, num2 + 1, num, num + 1);
				i++;
				num++;
				num2++;
			}
			t = RoundedCube.SetQuad(triangles, t, num2, num + 2, num, num + 1);
			int num3 = ring - 2;
			num2 -= this.xSize - 2;
			int num4 = num + 2;
			int j = 1;
			while (j < this.zSize - 1)
			{
				t = RoundedCube.SetQuad(triangles, t, num3, num2 + this.xSize - 1, num3 + 1, num2);
				int k = 1;
				while (k < this.xSize - 1)
				{
					t = RoundedCube.SetQuad(triangles, t, num2 + this.xSize - 1, num2 + this.xSize, num2, num2 + 1);
					k++;
					num2++;
				}
				t = RoundedCube.SetQuad(triangles, t, num2 + this.xSize - 1, num4 + 1, num2, num4);
				j++;
				num3--;
				num2++;
				num4++;
			}
			int num5 = num3 - 1;
			t = RoundedCube.SetQuad(triangles, t, num5 + 1, num5, num5 + 2, num2);
			int l = 1;
			while (l < this.xSize - 1)
			{
				t = RoundedCube.SetQuad(triangles, t, num5, num5 - 1, num2, num2 + 1);
				l++;
				num5--;
				num2++;
			}
			t = RoundedCube.SetQuad(triangles, t, num5, num5 - 1, num2, num5 - 2);
			return t;
		}

		private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
		{
			triangles[i] = v00;
			int arg_12_1 = i + 1;
			triangles[i + 4] = v01;
			triangles[arg_12_1] = v01;
			int arg_20_1 = i + 2;
			triangles[i + 3] = v10;
			triangles[arg_20_1] = v10;
			triangles[i + 5] = v11;
			return i + 6;
		}

		private void CreateColliders()
		{
			this.AddBoxCollider((float)this.xSize, (float)(this.ySize - this.roundness * 2), (float)(this.zSize - this.roundness * 2));
			this.AddBoxCollider((float)(this.xSize - this.roundness * 2), (float)this.ySize, (float)(this.zSize - this.roundness * 2));
			this.AddBoxCollider((float)(this.xSize - this.roundness * 2), (float)(this.ySize - this.roundness * 2), (float)this.zSize);
			Vector3 b = Vector3.one * (float)this.roundness;
			Vector3 vector = new Vector3((float)this.xSize, (float)this.ySize, (float)this.zSize) * 0.5f;
			Vector3 vector2 = new Vector3((float)this.xSize, (float)this.ySize, (float)this.zSize) - b;
			this.AddCapsuleCollider(0, vector.x, b.y, b.z);
			this.AddCapsuleCollider(0, vector.x, b.y, vector2.z);
			this.AddCapsuleCollider(0, vector.x, vector2.y, b.z);
			this.AddCapsuleCollider(0, vector.x, vector2.y, vector2.z);
			this.AddCapsuleCollider(1, b.x, vector.y, b.z);
			this.AddCapsuleCollider(1, b.x, vector.y, vector2.z);
			this.AddCapsuleCollider(1, vector2.x, vector.y, b.z);
			this.AddCapsuleCollider(1, vector2.x, vector.y, vector2.z);
			this.AddCapsuleCollider(2, b.x, b.y, vector.z);
			this.AddCapsuleCollider(2, b.x, vector2.y, vector.z);
			this.AddCapsuleCollider(2, vector2.x, b.y, vector.z);
			this.AddCapsuleCollider(2, vector2.x, vector2.y, vector.z);
		}

		private void AddBoxCollider(float x, float y, float z)
		{
			BoxCollider boxCollider = base.gameObject.AddComponent<BoxCollider>();
			boxCollider.size = new Vector3(x, y, z);
		}

		private void AddCapsuleCollider(int direction, float x, float y, float z)
		{
			CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = new Vector3(x, y, z);
			capsuleCollider.direction = direction;
			capsuleCollider.radius = (float)this.roundness;
			capsuleCollider.height = capsuleCollider.center[direction] * 2f;
		}
	}
}
