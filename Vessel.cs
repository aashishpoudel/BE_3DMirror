using System;
using System.Drawing;

namespace Graphic_Objects
{
	/// <summary>
	/// Contains Functions for drawing, rotating, scaling,... a Vessel.
	/// </summary>
	public class Vessel
	{
		public float height,heightOrg;
		private double radius,radiusOrg;
		const int s=2;
		const int v=1;
		private Point3D[] vtx=new Point3D[20*36*v*s];
		private Point3D[] temp=new Point3D[20*36*v*s];
		private Surface[] srf=new Surface[(20*v-1)*36*s];
		private Surface[] srf_sort=new Surface[(20*v-1)*36*s];
		private Surface srf_temp=new Surface();

		public Point3D position=new Point3D();
		public Point3D LightSrc=new Point3D(1,1,1);

		public Brush[] myBrush=new SolidBrush[1];
		public char RenderMode='S';
		bool reflect=false;
		public int diffuse=1;

		public Vessel(float a, float b, float c, float h, float r)
		{
			height=h*(1+c/(600-c));
			position=new Point3D(a,b,c);
			radius=r*(1+c/(600-c));
			
			heightOrg=height;
			radiusOrg=radius;

			CalcVertex();
		}
		public void CalcVertex()
		{
			double u=2*3.1416/36/s;
			double newRadius=new double();
			for(int i=0;i<3*v;i++)		//top
			{
				newRadius=radius+radius*2*Math.Tan((50-i*10/v)*3.1416/180);
				newRadius/=2;
				for(int j=0;j<36*s;j++)
				{
					vtx[36*s*i+j].X=(float) (newRadius*Math.Cos(u*j));
					vtx[36*s*i+j].Y=(float) (-height/2+i*height/(20*v-1));
					vtx[36*s*i+j].Z=(float) (newRadius*Math.Sin(-u*j));
					vtx[36*s*i+j].W=1;
				}
			}
			for(int i=3*v;i<17*v;i++)		//body
			{
				newRadius=radius*4*Math.Sin(2*(15+(i-4*v)*5/v)*3.1416/180);
				newRadius/=2;
				for(int j=0;j<36*s;j++)
				{
					vtx[36*s*i+j].X=(float) (newRadius*Math.Cos(u*j));
					vtx[36*s*i+j].Y=(float) (-height/2+i*height/(20*v-1));
					vtx[36*s*i+j].Z=(float) (newRadius*Math.Sin(-u*j));
					vtx[36*s*i+j].W=1;
				}
			}
			for(int i=17*v;i<20*v;i++)		//bottom
			{
				newRadius=radius*2*Math.Tan((45+(i-(v-1)-17*v)*10/v)*3.1416/180);
				newRadius/=2;
				for(int j=0;j<36*s;j++)
				{
					vtx[36*s*i+j].X=(float) (newRadius*Math.Cos(u*j));
					vtx[36*s*i+j].Y=(float) (-height/2+i*height/(20*v-1));
					vtx[36*s*i+j].Z=(float) (newRadius*Math.Sin(-u*j));
					vtx[36*s*i+j].W=1;
				}
			}
			for(int i=0;i<(20*v-1);i++)
			{
				int k=1;
				for(int j=0;j<36*s;j++)
				{
					if(j==(36*s-1)) k=-(36*s-1);
					srf[36*i*s+j].v0=36*s*i+j;
					srf[36*i*s+j].v1=36*i*s+j+k;
					srf[36*i*s+j].v2=36*s*(i+1)+j+k;
					srf[36*i*s+j].v3=36*s*(i+1)+j;
					//if(j==35)
					//{	srf[j+36*i].v2=srf[0+36*i].v1;	srf[j+36*i].v3=srf[0+36*i].v0; }
				}
			}
			vtx.CopyTo(temp,0);
		}
		
		public bool Is_Within()
		{	
			bool flaginside=true;
			for(int i=0; i<20*36*v*s; i++)
			{
				if((vtx[i].X+position.X)<150 ||(vtx[i].X+position.X)>400 || (vtx[i].Y+position.Y)<50 || (vtx[i].Y+position.Y)>300) 
				{	flaginside=false; break;	}
			}
			return flaginside;
		}


		public void Draw(Bitmap bmpSurface)
		{
			Graphics objG1 = Graphics.FromImage(bmpSurface);
			if(this.RenderMode=='S')
			{
				this.findABCD();
				this.correct();
				iCalculation();
			}
			srf.CopyTo(srf_sort,0);
			sort();
			PointF[] pts=new PointF[4];
			for(int i=0;i<((20*v-1)*36*s);i++)
			{				
				pts[0]= new PointF( vtx[srf_sort[i].v0].X+position.X , vtx[srf_sort[i].v0].Y+position.Y);
				pts[1]= new PointF( vtx[srf_sort[i].v1].X+position.X , vtx[srf_sort[i].v1].Y+position.Y);
				pts[2]= new PointF( vtx[srf_sort[i].v2].X+position.X , vtx[srf_sort[i].v2].Y+position.Y);
				pts[3]= new PointF( vtx[srf_sort[i].v3].X+position.X , vtx[srf_sort[i].v3].Y+position.Y);
					
				if(this.RenderMode=='W' )
				{
					objG1.DrawPolygon(Pens.Red,pts);
				}
				if(this.RenderMode=='S')
				{
						myBrush[0]=new SolidBrush(srf_sort[i].clr);
						objG1.FillPolygon(myBrush[0],pts);
				}
			}
			/*
			if(this.RenderMode=='S')
			{
				for(int i=0;i<19*36+34;i++)
				{				
					pts[0]= new PointF( vtx[srf[i].v0].X+position.X , vtx[srf[i].v0].Y+position.Y);
					pts[1]= new PointF( vtx[srf[i].v1].X+position.X , vtx[srf[i].v1].Y+position.Y);
					pts[2]= new PointF( vtx[srf[i].v2].X+position.X , vtx[srf[i].v2].Y+position.Y);
					pts[3]= new PointF( vtx[srf[i].v3].X+position.X , vtx[srf[i].v3].Y+position.Y);
					if(srf[i].C>0)
					{
						myBrush[0]=new SolidBrush(srf[i].clr);
						objG1.FillPolygon(myBrush[0],pts);
					}
				}
			}
			*/
			objG1.DrawImage(bmpSurface, 0,0,bmpSurface.Width,bmpSurface.Height);
			objG1.Dispose();
		}

		
		void correct()
		{
			for(int i=0; i<((20*v-1)*36*s); i++)
			{
				if(srf[i].C<0) { srf[i].A=-srf[i].A; srf[i].B=-srf[i].B; srf[i].C=-srf[i].C; }
			}
		}


		public void sort()
		{
			for(int i=0; i<((20*v-1)*36*s-1); i++)
			{
				for(int j=i+1; j<(20*v-1)*36*s; j++)
				{
					if(srf_sort[i].depth<srf_sort[j].depth) 
					{
						srf_temp=srf_sort[i];      //structure are value type unlike classes
						srf_sort[i]=srf_sort[j];
						srf_sort[j]=srf_temp;
					}
				}
			}
		}

		public void findABCD()
		{
			Point3D v1,v2,v3,v4;
			for(int i=0;i<((20*v-1)*36*s);i++)
			{			
				v1=vtx[ srf[i].v0 ];
				v2=vtx[ srf[i].v1 ];
				v3=vtx[ srf[i].v2 ];
				v4=vtx[ srf[i].v3 ];
				srf[i].D = v1.X*(v2.Y*v3.Z-v3.Y*v2.Z);
				srf[i].D += v2.X*(v3.Y*v1.Z-v1.Y*v3.Z);
				srf[i].D += v3.X*(v1.Y*v2.Z-v2.Y*v1.Z);
				srf[i].A = v1.Y*(v2.Z-v3.Z)+v2.Y*(v3.Z-v1.Z)+v3.Y*(v1.Z-v2.Z);
				srf[i].B = v1.Z*(v2.X-v3.X)+v2.Z*(v3.X-v1.X)+v3.Z*(v1.X-v2.X);
				srf[i].C = v1.X*(v2.Y-v3.Y)+v2.X*(v3.Y-v1.Y)+v3.X*(v1.Y-v2.Y);
				srf[i].depth=(v1.Z+v2.Z+v3.Z+v4.Z)/4;
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
		}//end of rotate

		void MatMultiply(float[,] first)
		{
			int i,n;
			float[,] result=new float[4,1];
			float[,] result_temp=new float[4,1]; 

			for(n=0;n<20*36*v*s;n++)
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
				vtx[n].X=result[0,0];  temp[n].X=result_temp[0,0]; 
				vtx[n].Y=result[1,0];  temp[n].Y=result_temp[1,0];
				vtx[n].Z=result[2,0];  temp[n].Z=result_temp[2,0];
				vtx[n].W=result[3,0];  temp[n].W=result_temp[3,0];
			}
		}//end of Matmultiply

		void iCalculation()
		{
			for(int i=0;i<(20*v-1)*36*s;i++)
			{
				float absn,dot,diff=0,L,Spec,SpecMod;	//Lx is opposite
				
				float Lx=-LightSrc.X;
				float Ly=LightSrc.Y;
				float Lz=LightSrc.Z; 
				
				if(reflect==true)	Lz=-Lz;

				absn=(float) Math.Sqrt(srf[i].A*srf[i].A+srf[i].B*srf[i].B+srf[i].C*srf[i].C);
				dot=(srf[i].A*Lx+srf[i].B*Ly+srf[i].C*Lz);
				SpecMod=(float)Math.Sqrt((srf[i].A-Lx)*(srf[i].A-Lx)+(srf[i].B-Ly)*(srf[i].B-Ly)+(srf[i].C-Lz)*(srf[i].C-Lz));

				L=(float) Math.Sqrt(Lx*Lx+Ly*Ly+Lz*Lz);
				if(dot<0)	dot=0;
				if(L==0)	L=1;
				if(absn!=0)
				{
					diff=dot/(L*absn);
					//Spec=(2*diff*srf[i].C)/absn;
				}
				else 
				{
					diff=0;
					Spec=0;
				}
				Spec=2*diff*srf[i].C/SpecMod-Lz/SpecMod;
				if (diff<0)diff=0;		if (diff>1)diff=1;
				if (Spec<0)Spec=0;		if (Spec>1)Spec=1;
				
				if(diffuse==1)
				{
					int red = (int) (100+diff*120);
					int green = (int) (100+diff*150);
					int blue = (int) (20+diff*10);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
				else if(diffuse==0)
				{
					int red = (int) (100+Spec*120);
					int green = (int) (100+Spec*150);
					int blue = (int) (20+Spec*10);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
				else
				{
					int red = (int) (100+Spec*85+diff*35);
					int green = (int) (100+Spec*120+diff*30);
					int blue = (int) (20+Spec*10+diff*10);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
			}
		}//end of icalculation


		public void Mirror()
		{
			reflect=true;
		}

		public void Scale(float sc)
		{
			for(int i=0;i<20*36*v*s;i++)
			{
				vtx[i].X=sc*temp[i].X;
				vtx[i].Y=sc*temp[i].Y;
				vtx[i].Z=sc*temp[i].Z;
				vtx[i].W=sc*temp[i].W;
			}
			height=sc*heightOrg;
			radius=sc*radiusOrg;
		}
	}
}
