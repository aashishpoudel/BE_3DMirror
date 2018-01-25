using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Graphic_Objects
{
	/// <summary>
	/// Background (class) - to draw background of my graphics project.
	/// </summary>	
	public class background
	{
		private Graphics objG1;
		Point[] leftFace=new Point[4];
        Point[] rightFace = new Point[4];
	    Point[] backFace = new Point[4];
        Point[] btmFace=new Point[4];
        Point[] upFace = new Point[4];

		
        Point[] mirror=new Point[4];
		Point[] border=new Point[4];
		LinearGradientBrush[] myBrush=new LinearGradientBrush[6];
		
		public background()
		{
            leftFace[0] = new Point(0, 0); leftFace[1] = new Point(150, 90);
			leftFace[2]=new Point(150,550);	leftFace[3]=new Point(0,768);
            //myBrush[0] = new LinearGradientBrush(new Rectangle(leftFace[0], new Size(150,740)),
            //     Color.SaddleBrown, Color.Maroon, LinearGradientMode.Horizontal);
            myBrush[0] = new LinearGradientBrush(new Rectangle(leftFace[0], new Size(150, 768)),
                 Color.Olive, Color.GreenYellow, LinearGradientMode.Horizontal);
            
			btmFace[0]=new Point(150,550);	btmFace[1]=new Point(874,550);
			btmFace[2]=new Point(1024,768);	btmFace[3]=new Point(0,768);
            //myBrush[2] = new LinearGradientBrush(new Rectangle(btmFace[3], new Size(874,200)),
            //    Color.Maroon, Color.SandyBrown, LinearGradientMode.Vertical);
            myBrush[2] = new LinearGradientBrush(new Rectangle(btmFace[3], new Size(1024,218)),
                Color.GreenYellow, Color.LightYellow, LinearGradientMode.Vertical);

            upFace[0] = new Point(0, 0); upFace[1] = new Point(1024, 0);
            upFace[2] = new Point(874, 90); upFace[3] = new Point(150, 90);
            myBrush[5] = new LinearGradientBrush(new Rectangle(upFace[1], new Size(1024, 90)),
                Color.LightYellow, Color.GreenYellow, LinearGradientMode.Vertical);
                //Color.LightGray, Color.SandyBrown, LinearGradientMode.Vertical);

            rightFace[0] = new Point(874, 0); rightFace[1] = new Point(1024, 0);
            rightFace[2] = new Point(1024, 768); rightFace[3] = new Point(874, 550);
            myBrush[4] = new LinearGradientBrush(new Rectangle(rightFace[0], new Size(150, 740)),
               Color.GreenYellow, Color.Olive, LinearGradientMode.Horizontal);

            backFace[0] = new Point(150, 0); backFace[1] = new Point(874, 0);
            backFace[2] = new Point(874, 550); backFace[3] = new Point(150, 550);
            myBrush[1] = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(810, 370)),
                Color.SandyBrown, Color.SandyBrown, LinearGradientMode.Horizontal);

			mirror[0]=new Point(160,100);	mirror[1]=new Point(864,100);
			mirror[2]=new Point(864,540);	mirror[3]=new Point(160,540);
			myBrush[3] = new LinearGradientBrush(new Rectangle(mirror[0], new Size(704,440)),
				Color.White, Color.White, LinearGradientMode.BackwardDiagonal);
		}
		
		public void Draw(Bitmap bmpSurface)
		{
            
			objG1 = Graphics.FromImage(bmpSurface);
            //System.Windows.Forms.MessageBox.Show(bmpSurface.Width.ToString());
          
			objG1.FillPolygon(myBrush[0],leftFace);
            objG1.FillPolygon(myBrush[4],rightFace);
             
			objG1.FillPolygon(myBrush[1],backFace);
			objG1.FillPolygon(myBrush[2],btmFace);
            objG1.FillPolygon(myBrush[5], upFace); 
			
            Pen myPen=new Pen(Color.Black,5);
			Rectangle myRect=new Rectangle(160,100,704,440);
			myRect.Inflate(new Size(5,5));
			objG1.DrawRectangle(myPen,myRect);

            //System.Windows.Forms.MessageBox.Show(bmpSurface.Width.ToString());
			//objG1.DrawImage(bmpSurface, 0,0,bmpSurface.Width,bmpSurface.Height);
			objG1.Dispose();
		}

		public void DrawMirror(Bitmap bmpSurface)
		{
			objG1 = Graphics.FromImage(bmpSurface);
            
			
            objG1.FillPolygon(myBrush[3],mirror);
			objG1.DrawImage(bmpSurface, 0,0,bmpSurface.Width,bmpSurface.Height);
			objG1.Dispose();
		}
	}
}
