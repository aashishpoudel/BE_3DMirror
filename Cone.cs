using System;
using System.Drawing;

namespace Graphic_Objects
{
	/// <summary>
	/// Contains Functions for drawing, rotating, scaling,... a Cone.
	/// </summary>
	public class Cone
	{
		public float height, heightOrg;
		private float radius_base, radius_baseOrg;
		private float radius_top, radius_topOrg;
		private Point3D[] vtx=new Point3D[36*2];
		private Point3D[] temp=new Point3D[36*2];
		private Surface[] srf=new Surface[36+10+24];
		
		public Point3D position=new Point3D();
		public Point3D LightSrc=new Point3D(1,1,1);
		Brush[] myBrush=new SolidBrush[1];
		public char RenderMode='S';
		bool reflect=false;
		public int diffuse=1;
		
		public Cone()
		{
		}
	    public Cone(float a, float b, float c, float h, float rb,float rt)
		{
			position = new Point3D(a,b,c);
			this.radius_base = rb*(1+c/(600-c));
			this.radius_top=rt*(1+c/(600-c));
			height=h*(1+c/(600-c));
			
			heightOrg=height;
			radius_baseOrg=radius_base;
			radius_topOrg=radius_top;

			CalcVertex();
		}

		public void CalcVertex()
		{
			double u=2*3.1416/36;
			for(int i=0;i<36;i++)
			{
				/*if((int)radius_base==0)
				{
					vtx[i].X=0;
					vtx[i].Z=0;
				}
				else*/
				{
					vtx[i].X=(float) (radius_base*Math.Cos(u*i));
					vtx[i].Z=(float) (radius_base*Math.Sin(u*i));
				}
				vtx[i].Y=(float) (height/2);
				vtx[i].W=1;
				/*if((int)radius_top==0)
				{
					vtx[36+i].X=0;
					vtx[36+i].Z=0;
				}
				else*/
				{
					vtx[36+i].X=(float) (radius_top*Math.Cos(u*i));
					vtx[36+i].Z=(float) (radius_top*Math.Sin(u*i));	
				}
				vtx[36+i].Y=(float) (-height/2);
				vtx[36+i].W=1;
			}
			for(int i=0;i<36;i++)
			{
				srf[i].v0=i;
				srf[i].v1=36+i;
				srf[i].v2=36+1+i;
				srf[i].v3=1+i;
				if(i==36-1)
				{	srf[i].v2=36;	srf[i].v3=0;	}
			}
			srf[36].v0=0;  srf[36].v1=9;  srf[36].v2=18; srf[36].v3=27;
			srf[37].v0=63; srf[37].v1=54; srf[37].v2=45; srf[37].v3=36;
			int j=0;
			for(int i=38;i<42;i++)
			{
				srf[i].v0=j;	srf[i+4].v3=j+36;	j+=3;
				srf[i].v1=j;	srf[i+4].v2=j+36;	j+=3;
				srf[i].v2=j;	srf[i+4].v1=j+36;	j+=3;
				if(j+36>=72)	j=0;
				srf[i].v3=j;	srf[i+4].v0=j+36;
			}
			j=0;
			for(int i=46;i<58;i++)
			{
				srf[i].v0=j;	srf[i+12].v3=j+36;	j++;
				srf[i].v1=j;	srf[i+12].v2=j+36;	j++;
				srf[i].v2=j;	srf[i+12].v1=j+36;	j++;
				if(j+36>=72)	j=0;
				srf[i].v3=j;	srf[i+12].v0=j+36;
			}
			vtx.CopyTo(temp,0);
		}

		public bool Is_Within()
		{	
			bool flaginside=true;
			for(int i=0; i<36*2; i++)
			{
				if((vtx[i].X+position.X)<150 ||(vtx[i].X+position.X)>400 || (vtx[i].Y+position.Y)<50 || (vtx[i].Y+position.Y)>300) 
				{
					flaginside=false; break;
				}
			}
			return flaginside;
		}


		public void Draw(Bitmap bmpSurface)
		{
			Graphics objG1 = Graphics.FromImage(bmpSurface);
			if(this.RenderMode=='S')
			{
				this.findABCD();
				iCalculation();
			}
			PointF[] pts=new PointF[4];
			for(int i=0;i<70;i++)
			{				
				pts[0]= new PointF( vtx[srf[i].v0].X+position.X , vtx[srf[i].v0].Y+position.Y);
				pts[1]= new PointF( vtx[srf[i].v1].X+position.X , vtx[srf[i].v1].Y+position.Y);
				pts[2]= new PointF( vtx[srf[i].v2].X+position.X , vtx[srf[i].v2].Y+position.Y);
				pts[3]= new PointF( vtx[srf[i].v3].X+position.X , vtx[srf[i].v3].Y+position.Y);
					
				if(this.RenderMode=='W' && i<36)
				{
					objG1.DrawPolygon(Pens.Red,pts);
				}
				if(this.RenderMode=='S')
				{
					if(srf[i].C<0)
					{
						myBrush[0]=new SolidBrush(srf[i].clr);
						objG1.FillPolygon(myBrush[0],pts);
					}
				}
			}
			if(this.RenderMode=='S')
			{
				for(int i=0;i<70;i++)
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
            objG1.DrawImage(bmpSurface, 0,0,bmpSurface.Width,bmpSurface.Height);
			objG1.Dispose();
		}

		public void findABCD()
		{
			Point3D v1,v2,v3;
			for(int i=0;i<70;i++)
			{
				v1=vtx[ srf[i].v0 ];
				v2=vtx[ srf[i].v1 ];
				v3=vtx[ srf[i].v2 ];
				
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
		}//end of rotate

		void MatMultiply(float[,] first)
		{
			int i,n;
			float[,] result=new float[4,1];
			float[,] result_temp=new float[4,1]; 

			for(n=0;n<36*2;n++)
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
				vtx[n].X=result[0,0];      temp[n].X=result_temp[0,0]; 
				vtx[n].Y=result[1,0];      temp[n].Y=result_temp[1,0];
				vtx[n].Z=result[2,0];      temp[n].Z=result_temp[2,0];
				vtx[n].W=result[3,0];      temp[n].W=result_temp[3,0];
			}
		}//end of Matmultiply

		void iCalculation()
		{
			for(int i=0;i<70;i++)
			{
				float absn,dot,diff=0,L,Spec;	//Lx is opposite
				
				float Lx=-LightSrc.X;
				float Ly=LightSrc.Y;
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
					Spec=(2*diff*srf[i].C)/absn-Lz/absn;
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
					int red = (int) (100+diff*100);
					int green = (int) (50+diff*50);
					int blue = (int) (100+diff*150);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
				else if(diffuse==0)
				{
					int red = (int) (100+Spec*100);
					int green = (int) (50+Spec*50);
					int blue = (int) (100+Spec*150);
					srf[i].clr=Color.FromArgb(red,green,blue);
				}
				else
				{
					int red = (int) (100+Spec*75+diff*25);
					int green = (int) (50+Spec*25+diff*25);
					int blue = (int) (100+Spec*100+diff*50);
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
			for(int i=0;i<72;i++)
			{
				vtx[i].X=s*temp[i].X;
				vtx[i].Y=s*temp[i].Y;
				vtx[i].Z=s*temp[i].Z;
				vtx[i].W=s*temp[i].W;
			}
			height=s*heightOrg;
			radius_base=s*radius_baseOrg;
			radius_top=s*radius_topOrg;
		}
	}
}
