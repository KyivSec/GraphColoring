namespace GraphColoring {

    internal class MyPoints : IDisposable {

        bool IsDisposed = false;
        public MyPoints() => DrawingPoints = new List<DrawingPoint>();
        public List<DrawingPoint> DrawingPoints { get; set; }

        public void Add(DrawingPoint NewPoint) {

            if (NewPoint.Dot.Size.Width > 1 && NewPoint.Dot.Size.Height > 1) {
                DrawingPoints.Add(NewPoint);
            }

        }

        public void Clear() {
            this.Dispose();
            this.DrawingPoints.Clear();
            this.DrawingPoints = new List<DrawingPoint>();
        }

        public void Remove(Point point) {
            Remove(this.DrawingPoints.Select((p, i) => { if (p.Dot.Contains(point)) return i; return -1; }).First());
        }

        public void Remove(int Index) {

            if (Index > -1) {
                this.DrawingPoints[Index].Delete();
                this.DrawingPoints.RemoveAt(Index);
            }

        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            DrawingPoints.RemoveAll(item => item == null);
        }

        protected void Dispose(bool IsSafeDisposing) {

            if (IsSafeDisposing && (!this.IsDisposed) && (this.DrawingPoints.Count > 0)) {
                foreach (DrawingPoint dp in this.DrawingPoints)
                    if (dp != null) dp.Delete();
            }

        }

        public class DrawingPoint {

            Pen m_Pen;
            Rectangle m_Dot = Rectangle.Empty;

            public bool DeletionQueued = false;
            public List<DrawingPoint> AdjacentPoints { get; set; }
            public int Color { get; set; }
            public int Index = 0;

            public DrawingPoint() : this(Point.Empty) { }
            public DrawingPoint(Point newPoint) {
                this.m_Pen = new Pen(System.Drawing.Color.Black, 1);
                this.m_Dot = new Rectangle(newPoint, new Size(2, 2));
                AdjacentPoints = new List<DrawingPoint>();
            }

            public Pen DrawingPen { get => this.m_Pen; set => this.m_Pen = value; }
            public Rectangle Dot { get => this.m_Dot; set => this.m_Dot = value; }

            public void Delete() {
                if (this.m_Pen != null) this.m_Pen.Dispose();
            }

            public void AddAdjacentPoint(DrawingPoint point) {
                if (!AdjacentPoints.Contains(point)) {
                    AdjacentPoints.Add(point);
                }
            }

            public void RemoveAdjacentPoint(DrawingPoint point) {
                AdjacentPoints.Remove(point);
            }

        }

    }

    internal class MyLines : IDisposable {

        bool IsDisposed = false;
        public MyLines() => DrawingLines = new List<DrawingLine>();
        public List<DrawingLine> DrawingLines { get; set; }

        public void Add(DrawingLine newLine) {
            DrawingLines.Add(newLine);
        }

        public void Clear() {
            this.Dispose();
            this.DrawingLines.Clear();
            this.DrawingLines = new List<DrawingLine>();
        }

        public void Remove(Point point) {
            Remove(this.DrawingLines.Select((l, i) => { if (l.Line.Contains(point)) return i; return -1; }).First());
        }

        public void Remove(int Index) {
            if (Index > -1) {
                this.DrawingLines[Index].Delete();
                this.DrawingLines.RemoveAt(Index);
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            DrawingLines.RemoveAll(item => item == null);
        }

        protected void Dispose(bool IsSafeDisposing) {
            if (IsSafeDisposing && (!this.IsDisposed) && (this.DrawingLines.Count > 0)) {
                foreach (DrawingLine dl in this.DrawingLines)
                    if (dl != null) dl.Delete();
            }
        }

        public class DrawingLine {

            Pen m_Pen;
            Point m_StartPoint = Point.Empty;
            Point m_EndPoint = Point.Empty;
            public bool DeletionQueued = false;

            public DrawingLine() : this(Point.Empty, Point.Empty) { }
            public DrawingLine(Point startPoint, Point endPoint) {
                this.m_Pen = new Pen(Color.Black, 1);
                this.m_StartPoint = startPoint;
                this.m_EndPoint = endPoint;
            }

            public Pen DrawingPen { get => this.m_Pen; set => this.m_Pen = value; }
            public Point StartPoint { get => this.m_StartPoint; set => this.m_StartPoint = value; }
            public Point EndPoint { get => this.m_EndPoint; set => this.m_EndPoint = value; }

            public Rectangle Line {

                get {

                    int minX = Math.Min(m_StartPoint.X, m_EndPoint.X);
                    int minY = Math.Min(m_StartPoint.Y, m_EndPoint.Y);
                    int maxX = Math.Max(m_StartPoint.X, m_EndPoint.X);
                    int maxY = Math.Max(m_StartPoint.Y, m_EndPoint.Y);
                    return new Rectangle(minX, minY, maxX - minX, maxY - minY);

                }

            }

            public void Delete() {
                if (this.m_Pen != null) this.m_Pen.Dispose();
            }

        }

    }

    internal static class Program {

        static void Main() {

            //Run code
            ApplicationConfiguration.Initialize();
            Application.Run(new GraphColoringInterface());

        }

    }

}