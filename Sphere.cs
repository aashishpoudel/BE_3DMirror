using System;
using System.Drawing;

namespace Graphic_Objects
{
	/// <summary>
	/// Contains Functions for drawing, rotating, scaling,... a Sphere.
	/// </summary>
	public class Sphere
	{
		public float radius;
		float radiusOrg;
		const int a=16;
        static int jpeg_count = 0;
        string filename_string;

		private Point3D[] vtx=new Point3D[200*a*a+20*a];
		private Surface[] srf=new Surface[200*a*a];
		public Point3D position=new Point3D();
		public Point3D LightSrc=new Point3D(1,1,1);
		public float v;

		Brush[] myBrush=new SolidBrush[1];
		public char RenderMode='S';
		bool reflect=false;
		public int diffuse=1;
		private Point3D[] temp=new Point3D[200*a*a+20*a];

		public Sphere()
		{
			radius=40;
			radiusOrg=radius;
			CalcVertex();
		}
		public Sphere(float a,float b,float c,float rad)
		{
			position = new Point3D(a,b,c); //center point of a sphere
			radius = rad *(1+c/(600-c));
			radiusOrg=radius;
			CalcVertex();			
		}
        //i think vertex table and surface table
		public void CalcVertex()
		{
			double u=0.31416/a;
			for(int i=0;i<=10*a;i++)
			{
				for(int j=0;j<20*a;j++)
				{
					int n=20*a*i+j;
					vtx[n].X=(float) ( radius*Math.Sin(u*i)*Math.Sin(u*j) );
					vtx[n].Y=(float) ( radius*Math.Cos(u*i) );
					vtx[n].Z=(float) ( radius*Math.Sin(u*i)*Math.Cos(u*j) );
					vtx[n].W=1;
				}
			}

			for(int i=0;i<10*a;i++)
			{
				int k=1;
				for(int j=0;j<20*a;j++)
				{
					int n=20*i*a+j;
					if(j==(20*a-1)) k=-(20*a-1);
					
					srf[n].v0=20*a*i+j;
					srf[n].v1=20*a*i+j+k;
					srf[n].v2=20*a*(i+1)+j+k;
					srf[n].v3=20*a*(i+1)+j;
				}
			}
			vtx.CopyTo(temp,0);			
			v=Math.Abs(vtx[200].X-vtx[4].X);
		}


		public void Draw(Bitmap bmpSurface)
		{
            jpeg_count = jpeg_count + 1; 
            Graphics objG1 = Graphics.FromImage(bmpSurface);
			//objG1.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			if(this.RenderMode=='S')
			{
				this.findABCD();				
				this.iCalculation();
			}
			
			//Array.Sort(surf
			PointF[] pts=new PointF[4];
			for(int i=0;i<200*a*a;i++)
			{					
				pts[0]= new PointF( vtx[srf[i].v0].X+position.X , vtx[srf[i].v0].Y+position.Y);
				pts[1]= new PointF( vtx[srf[i].v1].X+position.X , vtx[srf[i].v1].Y+position.Y);
				pts[2]= new PointF( vtx[srf[i].v2].X+position.X , vtx[srf[i].v2].Y+position.Y);
				pts[3]= new PointF( vtx[srf[i].v3].X+position.X , vtx[srf[i].v3].Y+position.Y);
				
				if(this.RenderMode=='W')
				{
					objG1.DrawPolygon(Pens.Red,pts);
				}
				if(this.RenderMode=='S')
				{
					if(srf[i].C>0)
					{
						myBrush[0]=new SolidBrush(srf[i].clr);
						objG1.FillPolygon(myBrush[0],pts);
					}
				}
				
			}
			objG1.DrawImage(bmpSurface, 0,0,bmpSurface.Width,bmpSurface.Height);
			objG1.Dispose();
            filename_string = string.Concat("angirash", jpeg_count.ToString(),".jpeg");
            if (jpeg_count%2==0)               
                bmpSurface.Save(filename_string, System.Drawing.Imaging.ImageFormat.Png);
            
            

		}

		public bool Is_Inside(int x, int y)
		{
			bool flag=false;
			if (((x-position.X)*(x-position.X)+(y-position.Y)*(y-position.Y)-radius*radius)<0.0) flag=true;
			//else flag=false;
			return flag;
		}
				
		public bool Is_Within()
		{	
			bool flaginside=true;
			for(int i=0; i<8; i++)
			{
				if((position.X-radius)<150 ||(position.X+radius)>400 || (position.Y-radius)<50 || (position.Y+radius)>300) 
				{
					flaginside=false; break;
				}
			}
			return flaginside;
		}

		public void findABCD()
		{
			Point3D v1,v2,v3;
			for(int i=0;i<200*a*a;i++)
			{			
				if(i>20*a)
				{
					v1=vtx[ srf[i].v0 ];
					v2=vtx[ srf[i].v1 ];
					v3=vtx[ srf[i].v2 ];
				}
				else
				{
					v1=vtx[ srf[i].v1 ];
					v2=vtx[ srf[i].v2 ];
					v3=vtx[ srf[i].v3 ];
				}

				srf[i].D = v1.X*(v2.Y*v3.Z-v3.Y*v2.Z);
				srf[i].D += v2.X*(v3.Y*v1.Z-v1.Y*v3.Z);
				srf[i].D += v3.X*(v1.Y*v2.Z-v2.Y*v1.Z);
				srf[i].A = v1.Y*(v2.Z-v3.Z)+v2.Y*(v3.Z-v1.Z)+v3.Y*(v1.Z-v2.Z);
				srf[i].B = v1.Z*(v2.X-v3.X)+v2.Z*(v3.X-v1.X)+v3.Z*(v1.X-v2.X);
				srf[i].C = v1.X*(v2.Y-v3.Y)+v2.X*(v3.Y-v1.Y)+v3.X*(v1.Y-v2.Y);
				srf[i].depth=(v1.Z+v2.Z+v3.Z)/3;
			}

				//Array.Sort(srf[0].depth,srf_sort[0]);


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
			v=Math.Abs(vtx[200].X-vtx[4].X);
		}//end of rotate

		void MatMultiply(float[,] first)
		{
			int i, n;
			float[,] result=new float[4,1];
			float[,] result_temp=new float[4,1]; 

			for(n=0;n<200*a*a+20*a;n++)
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
				vtx[n].X=result[0,0];     temp[n].X=result_temp[0,0];
				vtx[n].Y=result[1,0];	  temp[n].Y=result_temp[1,0];
				vtx[n].Z=result[2,0];	  temp[n].Z=result_temp[2,0];
				vtx[n].W=result[3,0];	  temp[n].W=result_temp[3,0];
			}
		}//end of Matmultiply

		void iCalculation()
		{
			int[] red=new int[200*a*a];
			int[] green=new int[200*a*a];
			int n=0;
			int[] blue=new int[200*a*a];
			for(int i=0;i<200*a*a;i++)
			{
				float absn,dot,diff=0,L,Spec,SpecMod;
				
				float Lx=-LightSrc.X;	//Lx is opposite
				float Ly=LightSrc.Y;
				float Lz=LightSrc.Z;
				
				if(reflect==true)	Lz=-Lz;

				absn=(float) Math.Sqrt(srf[i].A*srf[i].A+srf[i].B*srf[i].B+srf[i].C*srf[i].C);
				dot=(srf[i].A*Lx+srf[i].B*LightSrc.Y+srf[i].C*Lz);
				L=(float) Math.Sqrt(Lx*Lx+LightSrc.Y*LightSrc.Y+Lz*Lz);
				SpecMod=(float)Math.Sqrt((srf[i].A-Lx)*(srf[i].A-Lx)+(srf[i].B-Ly)*(srf[i].B-Ly)+(srf[i].C-Lz)*(srf[i].C-Lz));
				if(dot<0)	dot=0;
				if(L==0)	L=1;
				if(absn!=0)
				{
					diff=dot/(L*absn);
					Spec=(2*diff*srf[i].C)/absn;
					
				}
					
				else 
				{
					diff=0;
					Spec=0;
				}
				//Spec=2*diff*srf[i].C/SpecMod;//-Lz/SpecMod;
				//Spec=(float)Math.Pow(Spec,16);
				// V=(0,0,1) V.N=C  V.L=Lz
				if (diff<0)diff=0;		if (diff>1)diff=1;
				if (Spec<0)Spec=0;		if (Spec>1)Spec=1;
				
				if(diffuse==1)
				{
                     //red[i] = (int) (40+diff*90);
                     //green[i] = (int) (100+diff*150);
                     //blue[i] = (int) (40+diff*90);
                     red[i] = (int)(105 + diff * 150);
                     green[i] = (int)(100 + diff * 40); 
                     blue[i] = (int)(0 + diff * 0);
					 srf[i].clr=Color.FromArgb(red[i],green[i],blue[i]);
				}
				else if(diffuse==0)
				{
                    //red[i] = (int) (40+Spec*90);
                    //green[i] = (int) (100+Spec*150);
                    //blue[i] = (int) (40+Spec*90);
                    green[i] = (int)(40 + Spec * 90);
                    red[i] = (int)(100 + Spec * 150);
                    blue[i] = (int)(40 + Spec * 90);
					srf[i].clr=Color.FromArgb(red[i],green[i],blue[i]);
				}
				else
				{
                    //red[i] = (int) (40+Spec*40+diff*50);
                    //green[i] = (int) (100+Spec*100+diff*50);
                    //blue[i] = (int) (40+Spec*40+diff*50);
                    green[i] = (int)(40 + Spec * 40 + diff * 50);
                    red[i] = (int)(100 + Spec * 100 + diff * 50);
                    blue[i] = (int)(40 + Spec * 40 + diff * 50);
					srf[i].clr=Color.FromArgb(red[i],green[i],blue[i]);
				}
                				
			}
			
			for(int i=23; i<=56; i++)
			{
                //labeling part of the sphere to give viewer exact idea of the parts
				if(reflect==false)
				
                for(int j=0; j<=95; j++)
				{
                    
                    n=20*a*i+j;
                    if(i==23)
                        if (j >= 5 && j <= 9) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 24)
                        if ((j >= 3 && j <= 11) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 58 && j <= 59)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 25)
                        if ((j >= 2 && j <= 12) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89)||(j >= 57 && j <= 59)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 26)
                        if ((j >= 1 && j <= 4) || (j >= 9 && j <= 13) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89)||(j >= 56 && j <= 58)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 27)
                        if ((j >= 0 && j <= 3) || (j >= 11 && j <= 14) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89)||(j >= 55 && j <= 57)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 28)
                        if ((j >= 0 && j <= 2) || (j >= 12 && j <= 14) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 54 && j <= 56)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 29)
                        if ((j >= 12 && j <= 14) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 53 && j <= 55)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 30)
                        if ((j >= 12 && j <= 14) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 52 && j <= 54)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 31)
                        if ((j >= 12 && j <= 14) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 51 && j <= 56) || (j >= 79 && j <= 85)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 32)
                        if ((j >= 11 && j <= 13) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 50 && j <= 57) || (j >= 78 && j <= 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 33)
                        if ((j >= 10 && j <= 12) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 87 && j <= 89) || (j >= 49 && j <= 58) || (j >= 77 && j <= 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 34)
                        if ((j >= 9 && j <= 11) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 49 && j <= 51) || (j >= 56 && j <= 59) || (j >= 76 && j <= 79)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 35)
                        if ((j==50) || (j >= 5 && j <= 19) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 57 && j <= 60) || (j >= 75 && j <= 78) || (j >= 84 && j <= 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 36)
                        if ((j >= 5 && j <= 19) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 58 && j <= 60) || (j >= 75 && j <= 77) || (j >= 83 && j <= 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 37)
                        if ((j >= 8 && j <= 10) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 58 && j <= 60) || (j >= 75 && j <= 77) || (j >= 82 && j <= 85)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 38)
                        if ((j >= 9 && j <= 11) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 51 && j <= 55) || (j >= 58 && j <= 60) || (j >= 75 && j <= 77) || (j >= 81 && j <= 84)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 39)
                        if ((j >= 10 && j <= 12) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 50 && j <= 56) || (j >= 58 && j <= 60) || (j >= 75 && j <= 77) || (j >= 80 && j <= 83)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 40)
                        if ((j >= 11 && j <= 13) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 49 && j <= 60) || (j >= 75 && j <= 77) || (j >= 79 && j <= 82)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 41)
                        if ((j >= 11 && j <= 13) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 49 && j <= 51) || (j >= 56 && j <= 60) || (j >= 75 && j <= 81)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 42)
                        if ((j >= 11 && j <= 13) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 49 && j <= 50) || (j >= 57 && j <= 60) || (j >= 75 && j <= 80)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 43)
                        if ((j >= 11 && j <= 13) || (j >= 20 && j <= 23) || (j >= 29 && j <= 31) || (j >= 39 && j <= 41) || (j >= 65 && j <= 67) || (j >= 85 && j <= 89) || (j >= 49 && j <= 50) || (j >= 57 && j <= 60) || (j >= 75 && j <= 79)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 44)
                        if (j == 3 || (j >= 10 && j <= 13) || (j >= 17 && j <= 47) || (j >= 49 && j <= 58) || (j >= 56 && j <= 59) || (j >= 62 && j <= 91)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 45)
                        if ((j >= 3 && j<=12) || (j >= 17 && j <= 47) || (j >= 49 && j <= 58) || (j >= 62 && j <= 91)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 46)
                        if ((j >= 3 && j <= 12) || (j >= 17 && j <= 47) || (j >= 50 && j <= 57) || (j >= 62 && j <= 91)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]); 
                    if (i == 47)
                        if ((j >= 4 && j <= 10) || (j >= 37 && j <= 40) || (j >= 51 && j <= 56) || (j >= 63 && j <= 66)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 48)
                        if ((j >= 37 && j <= 39) || (j >= 62 && j <= 65)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 49)
                        if ((j >= 37 && j <= 39) || (j >= 60 && j <= 64)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 50)
                        if ((j >= 37 && j <= 39) || (j >= 58 && j <= 63)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 51)
                        if ((j >= 37 && j <= 40) || (j >= 56 && j <= 61)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 52)
                        if ((j >= 38 && j <= 41) || (j >= 54 && j <= 59)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 53)
                        if ((j >= 39 && j <= 57)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 54)
                        if ((j >= 40 && j <= 55)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    if (i == 55)
                        if ((j >= 42 && j <= 52)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);                

                    #region E
                    //if (j == 0)
                    //    srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    
                    //if (i == 16)
                    //{                        
                    //    if (j <= 40)
                    //        srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);                        
                    //}

                    //if (i == 26)
                    //{
                    //    if (j <= 25)
                    //        srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    //}

                    //if (i == 36)
                    //{
                    //    if (j <= 40)
                    //        srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    //}
                    #endregion 
                    #region commented 1
                    //if(i==16) 
                    //    if(j==0 || (j>=5))
                    //        srf[n].clr=Color.FromArgb(red[n]/2,green[n]/2,blue[n]);
                    //if(i>=17 && i<=20)
                    //    if( j==0 ||j==5)
                    //        srf[n].clr=Color.FromArgb(red[n]/2,green[n]/2,blue[n]);
                    //if (i == 26)
                    //    srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    //if(i>=22 && i<=25)
                    //    if(j==5 || j==10)
                    //        srf[n].clr=Color.FromArgb(red[n]/2,green[n]/2,blue[n]);
                    //if(i==26)
                    //    if(j<=5 ||j==10)
                    //        srf[n].clr=Color.FromArgb(red[n]/2,green[n]/2,blue[n]);
                    #endregion 
                }
				if(reflect==true)
                    for (int j = 20 * a / 2; j >= 20 * a / 2 - 20 * a / 2 - 50; j--)
                    {
                        n = 20 * a * i + j;
                        if (i == 23)
                            if (j <= 20 * a / 2 - 5 && j >= 20 * a / 2 - 9) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 24)
                            if ((j <= 20 * a / 2 - 3 && j >= 20 * a / 2 - 11) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 58 && j >= 20 * a / 2 - 59)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 25)
                            if ((j <= 20 * a / 2 - 2 && j >= 20 * a / 2 - 12) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 57 && j >= 20 * a / 2 - 59)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 26)
                            if ((j <= 20 * a / 2 - 1 && j >= 20 * a / 2 - 4) || (j <= 20 * a / 2 - 9 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 56 && j >= 20 * a / 2 - 58)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 27)
                            if ((j <= 20 * a / 2 - 0 && j >= 20 * a / 2 - 3) || (j <= 20 * a / 2 - 11 && j >= 20 * a / 2 - 14) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 55 && j >= 20 * a / 2 - 57)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 28)
                            if ((j <= 20 * a / 2 - 0 && j >= 20 * a / 2 - 2) || (j <= 20 * a / 2 - 12 && j >= 20 * a / 2 - 14) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 54 && j >= 20 * a / 2 - 56)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 29)
                            if ((j <= 20 * a / 2 - 12 && j >= 20 * a / 2 - 14) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 53 && j >= 20 * a / 2 - 55)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 30)
                            if ((j <= 20 * a / 2 - 12 && j >= 20 * a / 2 - 14) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 52 && j >= 20 * a / 2 - 54)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 31)
                            if ((j <= 20 * a / 2 - 12 && j >= 20 * a / 2 - 14) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 51 && j >= 20 * a / 2 - 56) || (j <= 20 * a / 2 - 79 && j >= 20 * a / 2 - 85)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 32)
                            if ((j <= 20 * a / 2 - 11 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 50 && j >= 20 * a / 2 - 57) || (j <= 20 * a / 2 - 78 && j >= 20 * a / 2 - 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 33)
                            if ((j <= 20 * a / 2 - 10 && j >= 20 * a / 2 - 12) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 87 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 58) || (j <= 20 * a / 2 - 77 && j >= 20 * a / 2 - 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 34)
                            if ((j <= 20 * a / 2 - 9 && j >= 20 * a / 2 - 11) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 51) || (j <= 20 * a / 2 - 56 && j >= 20 * a / 2 - 59) || (j <= 20 * a / 2 - 76 && j >= 20 * a / 2 - 79)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 35)
                            if ((j == 50) || (j <= 20 * a / 2 - 5 && j >= 20 * a / 2 - 19) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 57 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 78) || (j <= 20 * a / 2 - 84 && j >= 20 * a / 2 - 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 36)
                            if ((j <= 20 * a / 2 - 5 && j >= 20 * a / 2 - 19) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 58 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 77) || (j <= 20 * a / 2 - 83 && j >= 20 * a / 2 - 86)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 37)
                            if ((j <= 20 * a / 2 - 8 && j >= 20 * a / 2 - 10) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 58 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 77) || (j <= 20 * a / 2 - 82 && j >= 20 * a / 2 - 85)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 38)
                            if ((j <= 20 * a / 2 - 9 && j >= 20 * a / 2 - 11) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 51 && j >= 20 * a / 2 - 55) || (j <= 20 * a / 2 - 58 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 77) || (j <= 20 * a / 2 - 81 && j >= 20 * a / 2 - 84)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 39)
                            if ((j <= 20 * a / 2 - 10 && j >= 20 * a / 2 - 12) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 50 && j >= 20 * a / 2 - 56) || (j <= 20 * a / 2 - 58 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 77) || (j <= 20 * a / 2 - 80 && j >= 20 * a / 2 - 83)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 40)
                            if ((j <= 20 * a / 2 - 11 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 77) || (j <= 20 * a / 2 - 79 && j >= 20 * a / 2 - 82)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 41)
                            if ((j <= 20 * a / 2 - 11 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 51) || (j <= 20 * a / 2 - 56 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 81)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 42)
                            if ((j <= 20 * a / 2 - 11 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 50) || (j <= 20 * a / 2 - 57 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 80)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 43)
                            if ((j <= 20 * a / 2 - 11 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 20 && j >= 20 * a / 2 - 23) || (j <= 20 * a / 2 - 29 && j >= 20 * a / 2 - 31) || (j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 65 && j >= 20 * a / 2 - 67) || (j <= 20 * a / 2 - 85 && j >= 20 * a / 2 - 89) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 50) || (j <= 20 * a / 2 - 57 && j >= 20 * a / 2 - 60) || (j <= 20 * a / 2 - 75 && j >= 20 * a / 2 - 79)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 44)
                            if (j == 3 || (j <= 20 * a / 2 - 10 && j >= 20 * a / 2 - 13) || (j <= 20 * a / 2 - 17 && j >= 20 * a / 2 - 47) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 58) || (j <= 20 * a / 2 - 56 && j >= 20 * a / 2 - 59) || (j <= 20 * a / 2 - 62 && j >= 20 * a / 2 - 91)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 45)
                            if ((j <= 20 * a / 2 - 3 && j >= 20 * a / 2 - 12) || (j <= 20 * a / 2 - 17 && j >= 20 * a / 2 - 47) || (j <= 20 * a / 2 - 49 && j >= 20 * a / 2 - 58) || (j <= 20 * a / 2 - 62 && j >= 20 * a / 2 - 91)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 46)
                            if ((j <= 20 * a / 2 - 3 && j >= 20 * a / 2 - 12) || (j <= 20 * a / 2 - 17 && j >= 20 * a / 2 - 47) || (j <= 20 * a / 2 - 50 && j >= 20 * a / 2 - 57) || (j <= 20 * a / 2 - 62 && j >= 20 * a / 2 - 91)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 47)
                            if ((j <= 20 * a / 2 - 4 && j >= 20 * a / 2 - 10) || (j <= 20 * a / 2 - 37 && j >= 20 * a / 2 - 40) || (j <= 20 * a / 2 - 51 && j >= 20 * a / 2 - 56) || (j <= 20 * a / 2 - 63 && j >= 20 * a / 2 - 66)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 48)
                            if ((j <= 20 * a / 2 - 37 && j >= 20 * a / 2 - 39) || (j <= 20 * a / 2 - 62 && j >= 20 * a / 2 - 65)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 49)
                            if ((j <= 20 * a / 2 - 37 && j >= 20 * a / 2 - 39) || (j <= 20 * a / 2 - 60 && j >= 20 * a / 2 - 64)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 50)
                            if ((j <= 20 * a / 2 - 37 && j >= 20 * a / 2 - 39) || (j <= 20 * a / 2 - 58 && j >= 20 * a / 2 - 63)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 51)
                            if ((j <= 20 * a / 2 - 37 && j >= 20 * a / 2 - 40) || (j <= 20 * a / 2 - 56 && j >= 20 * a / 2 - 61)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 52)
                            if ((j <= 20 * a / 2 - 38 && j >= 20 * a / 2 - 41) || (j <= 20 * a / 2 - 54 && j >= 20 * a / 2 - 59)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 53)
                            if ((j <= 20 * a / 2 - 39 && j >= 20 * a / 2 - 57)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 54)
                            if ((j <= 20 * a / 2 - 40 && j >= 20 * a / 2 - 55)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                        if (i == 55)
                            if ((j <= 20 * a / 2 - 42 && j >= 20 * a / 2 - 52)) srf[n].clr = Color.FromArgb(red[n] / 2, green[n] / 2, blue[n]);
                    }

			}

		}//end of icalculation

		public void Mirror()
		{
			reflect=true;
		}

		public void Scale(float s)
		{
			for(int i=0;i<200*a*a+20*a;i++)
			{
				vtx[i].X=s*temp[i].X;
				vtx[i].Y=s*temp[i].Y;
				vtx[i].Z=s*temp[i].Z;
				vtx[i].W=s*temp[i].W;
			}
			radius=radiusOrg*s;
			v=Math.Abs(vtx[200].X-vtx[4].X);
		}
	}
}
