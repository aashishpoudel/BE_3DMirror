using System;
using System.Drawing;

namespace Graphic_Objects
{
	/// <summary>
	/// Contains Functions for drawing, rotating, scaling,... a Cube.
	/// </summary>
	public class Cube
	{
		public float length;
		private Point3D[] vtx=new Point3D[8];
		private Point3D[] vtxP=new Point3D[8];
		private Surface[] srf=new Surface[6];
		private bool flag=false;		
		public Point3D position=new Point3D();
		public Point3D LightSrc=new Point3D(1,1,1);
		public float v;
		private Point3D[] temp=new Point3D[8];

		public char RenderMode='S';
		bool reflect=false;
		public int diffuse=1;
		Brush[] myBrush=new SolidBrush[6];


		public Cube()
		{
			length=80;
			CalcVertex();
		}
		public Cube(float a,float b,float c,float len)
		{
			position = new Point3D(a,b,c);
			length = len*(1+c/(600-c));
			CalcVertex();
		}
		public void CalcVertex()
		{
			float a=length/2;
			vtx[0]=new Point3D(a,a,a);
			vtx[1]=new Point3D(a,a,-a);
			vtx[2]=new Point3D(-a,a,-a);
			vtx[3]=new Point3D(-a,a,a);
			vtx[4]=new Point3D(-a,-a,a);
			vtx[5]=new Point3D(a,-a,a);
			vtx[6]=new Point3D(a,-a,-a);
			vtx[7]=new Point3D(-a,-a,-a);

			int[] st={0,1,2,3, 5,6,1,0, 4,7,6,5, 3,2,7,4, 2,1,6,7, 5,0,3,4};
			for(int i=0,n=-1;i<6;i++)
			{	
				srf[i].v0=st[++n];      //srf[i] ko 4 vertices haru
				srf[i].v1=st[++n];		
				srf[i].v2=st[++n];		
				srf[i].v3=st[++n];
			}
			vtx.CopyTo(temp,0);
			v=Math.Abs(vtx[0].X-vtx[4].X);
		}


		public void Draw(Bitmap bmpSurface)
		{
			vtx.CopyTo(vtxP,0);
			for(int i=0;i<8;i++)
			{
				float u=(0-vtx[i].Z)/(600-vtx[i].Z);
				//if(reflect==true)	u=-u;
				vtxP[i].X=vtxP[i].X*(1-u);
				vtxP[i].Y=vtxP[i].Y*(1-u);
				vtxP[i].Z=vtxP[i].Z-(vtxP[i].Z-600)*u;
			}

			Graphics objG1 = Graphics.FromImage(bmpSurface);
			//objG1.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			vtx.CopyTo(vtxP,0);
			for(int i=0;i<8;i++)
			{
				float u=(0-vtx[i].Z)/(600-vtx[i].Z);
				vtxP[i].X=vtxP[i].X*(1-u);
				vtxP[i].Y=vtxP[i].Y*(1-u);
				//	vtxP[i].Z=vtxP[i].Z-(vtxP[i].Z-600)*u;

				if(reflect==true)
				{
					vtxP[i].X=-vtxP[i].X*(1-u);
					vtxP[i].Y=-vtxP[i].Y*(1-u);
					//		vtxP[i].Z=-(vtxP[i].Z-(vtxP[i].Z-600)*u);
				}

			}
			if(this.RenderMode=='S')
			{
				this.findABCD();
				iCalculation();
			}
			PointF[] pts=new PointF[4];
			for(int i=0;i<6;i++)
			{
				pts[0]= new PointF( vtxP[srf[i].v0].X+position.X , vtxP[srf[i].v0].Y+position.Y);
				pts[1]= new PointF( vtxP[srf[i].v1].X+position.X , vtxP[srf[i].v1].Y+position.Y);
				pts[2]= new PointF( vtxP[srf[i].v2].X+position.X , vtxP[srf[i].v2].Y+position.Y);
				pts[3]= new PointF( vtxP[srf[i].v3].X+position.X , vtxP[srf[i].v3].Y+position.Y);
				
				if(this.RenderMode=='W')
				{
					objG1.DrawPolygon(Pens.Red,pts);
				}
				if(this.RenderMode=='S')
				{
					if(srf[i].C>0)
					{
						myBrush[i]=new SolidBrush(srf[i].clr);
						objG1.FillPolygon(myBrush[i],pts);
					}
				}
                
			}
			objG1.DrawImage(bmpSurface, 0,0,bmpSurface.Width,bmpSurface.Height);
			objG1.Dispose();
		}
		
		public bool Is_Inside(int x, int y)
		{
			if (((x-position.X)*(x-position.X)+(y-position.Y)*(y-position.Y)-length*length*.64)<0.0) flag=true;
			else flag=false;
			return flag;
		}
				
		public bool Is_Within()
		{	
			bool flaginside=true;
			for(int i=0; i<8; i++)
			{
				if((vtx[i].X+position.X)<150 ||(vtx[i].X+position.X)>400 || (vtx[i].Y+position.Y)<50 || (vtx[i].Y+position.Y)>300) 
				{
					flaginside=false; break;
				}
			}
			return flaginside;
		}

 		public void findABCD()
		{
			Point3D v1,v2,v3;
			for(int i=0;i<6;i++)
			{			
				v1=vtxP[ srf[i].v1 ];
				v2=vtxP[ srf[i].v2 ];
				v3=vtxP[ srf[i].v3 ];

				srf[i].D = v1.X*(v2.Y*v3.Z-v3.Y*v2.Z);
				srf[i].D += v2.X*(v3.Y*v1.Z-v1.Y*v3.Z);
				srf[i].D += v3.X*(v1.Y*v2.Z-v2.Y*v1.Z);
				srf[i].A = v1.Y*(v2.Z-v3.Z)+v2.Y*(v3.Z-v1.Z)+v3.Y*(v1.Z-v2.Z);
				srf[i].B = v1.Z*(v2.X-v3.X)+v2.Z*(v3.X-v1.X)+v3.Z*(v1.X-v2.X);
				srf[i].C = v1.X*(v2.Y-v3.Y)+v2.X*(v3.Y-v1.Y)+v3.X*(v1.Y-v2.Y);
			}
		}

		public void Rotation(double theta,char axis)
		{
			float[,] rot=new float[4,4];
			theta*=3.1416/180;
			int i,j;
			for(i=0;i<4;i++)
			{
				for(j=0;j<4;j++)
				{
					if(i==j)
						rot[i,j]=1;
					else
						rot[i,j]=0;
				}
			}
			switch(axis)
			{
				case  'Z': // Z-axis
				case  'z':
					if(axis=='Z')theta=-theta;
					rot[0,0]=(float)Math.Cos(theta);	rot[0,1]=(float)-Math.Sin(theta);
					rot[1,0]=(float)Math.Sin(theta);	rot[1,1]=(float)Math.Cos(theta);
					break;
				case 'X': //X-axis
				case 'x':
					if(axis=='X')theta=-theta;
					rot[1,1]=(float)Math.Cos(theta);	rot[1,2]=(float)-Math.Sin(theta);
					rot[2,1]=(float)Math.Sin(theta);	rot[2,2]=(float)Math.Cos(theta);
					break;
				case 'Y': //Y-axis
				case 'y':
					if(axis=='Y')theta=-theta;
					rot[0,0]=(float)Math.Cos(theta);	rot[0,2]=(float)Math.Sin(theta);
					rot[2,0]=(float)-Math.Sin(theta);	rot[2,2]=(float)Math.Cos(theta);
					break;
			}//end of switch
			MatMultiply(rot);
			v=Math.Abs(vtx[0].X-vtx[4].X);
		}//end of rotate

		void MatMultiply(float[,] first)
		{
			int i,n;
			float[,] result=new float[4,1];
			float[,] result_temp=new float[4,1];   //for temporary point to make its size constnat but all vertices rotated.

			for(n=0;n<8;n++)
			{
				for(i=0;i<4;i++)
				{
					result[i,0] = 0;
					result[i,0] = first[i,0]*vtx[n].X + first[i,1]*vtx[n].Y
						+ first[i,2]*vtx[n].Z + first[i,3]*vtx[n].W;
					result_temp[i,0] = 0;
					result_temp[i,0] = first[i,0]*temp[n].X + first[i,1]*temp[n].Y
						+ first[i,2]*temp[n].Z + first[i,3]*temp[n].W;

				}
				vtx[n].X=result[0,0];    temp[n].X=result_temp[0,0];
				vtx[n].Y=result[1,0];    temp[n].Y=result_temp[1,0];
				vtx[n].Z=result[2,0];    temp[n].Z=result_temp[2,0];
				vtx[n].W=result[3,0];    temp[n].W=result_temp[3,0];
			}
				
		}

	
		void iCalculation()
		{
			for(int i=0;i<6;i++)
			{
				float absn,dot,diff=0,L,Spec;	//Ly is opposite
				float Lx=LightSrc.X;
				float Ly=-LightSrc.Y;
				float Lz=LightSrc.Z; 
				if(reflect==true)	Lz=-Lz;

				absn=(float) Math.Sqrt(srf[i].A*srf[i].A+srf[i].B*srf[i].B+srf[i].C*srf[i].C);
				dot=(srf[i].A*Lx+srf[i].B*Ly+srf[i].C*Lz);
				L=(float) Math.Sqrt(Lx*Lx+Ly*Ly+Lz*Lz);
				if(dot<0)	dot=0;
				if(L==0)	L=1;
				if(absn!=0)
				{
					diff=dot/(L*absn);
					Spec=2*diff*srf[i].C/absn-Lz/L;
				}
				else 
				{
					diff=0;
					Spec=0;
				}
				if (diff<0)diff=0;		if (diff>1)diff=1;
				if (Spec<0)Spec=0;		if (Spec>1)Spec=1;
								
				if(diffuse==1)
				{
					int red = (int) (100+diff*150);
					int green = (int) (0+diff*100);
					int blue = (int) (0+diff*100);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
				else if(diffuse==0)
				{
					int red = (int) (100+Spec*150);
					int green = (int) (0+Spec*100);
					int blue = (int) (0+Spec*100);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
				else
				{
					int red = (int) (100+Spec*100+diff*50);
					int green = (int) (0+Spec*37+diff*37);
					int blue = (int) (0+Spec*37+diff*37);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
			}
		}//end of icalculation

		public void Mirror()
		{
			reflect=true;
		}

		public void Scale(float s)
		{
			
			for(int i=0;i<8;i++)
			{
				vtx[i].X=s*temp[i].X;
				vtx[i].Y=s*temp[i].Y;
				vtx[i].Z=s*temp[i].Z;
				vtx[i].W=s*temp[i].W;
			}
			length*=s;
			v=Math.Abs(vtx[0].X-vtx[4].X);
		}

		public void Translate(int a,int b,int c)
		{
			for(int i=0;i<8;i++)
			{
				position.X+=a;
				position.Y+=b;
				position.Z+=c;
			} 
		}

		public void Translate(Point3D p)
		{
			for(int i=0;i<8;i++)
			{
				position.X+=p.X;
				position.Y+=p.Y;
				position.Z+=p.Z;
			}
		}
		public void ShearXYZ(int Shx,int Shy,int Shz)
		{
			for(int i=0;i<8;i++)
			{
				vtx[i].X*=Shx;
				vtx[i].Y*=Shy;
				vtx[i].Z*=Shz;
			}
		}
	}
}
