using System;

namespace Graphic_Objects
{
	public struct Point3D
	{
		public float X,Y,Z,W;
		public Point3D(float a,float b,float c)
		{
			X=a; Y=b; Z=c; W=1;
		}
	}

	public struct Surface
	{
		public int v0,v1,v2,v3;
		public float A,B,C,D,depth;
		public System.Drawing.Color clr;
	}
}
