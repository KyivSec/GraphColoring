using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GraphColoring {

    public partial class GraphColoringInterface : Form {

        //Run UI
        public GraphColoringInterface() {
            InitializeComponent();
        }

        bool AddPointMode = false;
        bool RemovePointMode = false;
        bool AddConnectionMode = false;
        bool Colored = false;

        MyPoints myPoints = new MyPoints();
        MyLines myLines = new MyLines();
        Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
        Dictionary<int, int> coloringResults = new Dictionary<int, int>();

        MyPoints.DrawingPoint? FirstSelectedPoint;
        MyPoints.DrawingPoint? SecondSelectedPoint;

        int FirstSelectedPointIndex = 0;
        int SecondSelectedPointIndex = 0;

        Dictionary<MyLines.DrawingLine, (MyPoints.DrawingPoint, MyPoints.DrawingPoint)> ConnectedPoints = new Dictionary<MyLines.DrawingLine, (MyPoints.DrawingPoint, MyPoints.DrawingPoint)>();
        Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Purple, Color.Orange, Color.Black };

        public void backtrackMRVColoring()
        {
            if (Colored) { return; }

            int[] result = new int[myPoints.DrawingPoints.Count];

            for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
            {
                result[i] = -1;
            }

            backtrackMRVUtil(result, 0);

            for (int u = 0; u < myPoints.DrawingPoints.Count; u++)
            {
                myPoints.DrawingPoints[u].DrawingPen.Color = colors[result[u] % colors.Length];
                coloringResults[u] = result[u];
            }

            panel1.Invalidate();
            label2.Text = "Status: Colored";
        }

        bool backtrackMRVUtil(int[] result, int v)
        {
            if (v == myPoints.DrawingPoints.Count)
                return true;

            int minDegree = int.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
            {
                if (result[i] == -1)
                {
                    int degree = adj[i].Count;
                    if (degree < minDegree)
                    {
                        minDegree = degree;
                        minIndex = i;
                    }
                }
            }

            for (int c = 0; c < colors.Length; c++)
            {
                if (isSafe(minIndex, c, result))
                {
                    result[minIndex] = c;
                    if (backtrackMRVUtil(result, v + 1))
                        return true;
                    result[minIndex] = -1;
                }
            }

            return false;
        }

        public void backtrackPowerColoring()
        {
            if (Colored) { return; }

            int[] result = new int[myPoints.DrawingPoints.Count];

            for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
            {
                result[i] = -1;
            }

            backtrackPowerUtil(result, 0);

            for (int u = 0; u < myPoints.DrawingPoints.Count; u++)
            {
                myPoints.DrawingPoints[u].DrawingPen.Color = colors[result[u] % colors.Length];
                coloringResults[u] = result[u];
            }

            panel1.Invalidate();
            label2.Text = "Status: Colored";
        }

        bool backtrackPowerUtil(int[] result, int v)
        {
            if (v == myPoints.DrawingPoints.Count)
                return true;

            int maxPower = int.MinValue;
            int maxIndex = -1;

            for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
            {
                if (result[i] == -1)
                {
                    int power = getPower(i, result);
                    if (power > maxPower)
                    {
                        maxPower = power;
                        maxIndex = i;
                    }
                }
            }

            for (int c = 0; c < colors.Length; c++)
            {
                if (isSafe(maxIndex, c, result))
                {
                    result[maxIndex] = c;
                    if (backtrackPowerUtil(result, v + 1))
                        return true;
                    result[maxIndex] = -1;
                }
            }

            return false;
        }

        int getPower(int v, int[] result)
        {
            int power = 0;
            foreach (int i in adj[v])
            {
                if (result[i] != -1)
                    power++;
            }
            return power;
        }

        bool isSafe(int v, int c, int[] result)
        {
            foreach (int i in adj[v])
            {
                if (result[i] == c)
                    return false;
            }
            return true;
        }


        public void greedyColoring()
        {
            if (Colored) { return;  }

            int[] result = new int[myPoints.DrawingPoints.Count];

            for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
            {
                result[i] = -1;
            }

            result[0] = 0;

            bool[] available = new bool[myPoints.DrawingPoints.Count];

            for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
            {
                available[i] = true;
            }

            for (int u = 1; u < myPoints.DrawingPoints.Count; u++)
            {

                foreach (int i in adj[u])
                {
                    if (result[i] != -1)
                        available[result[i]] = false;
                }

                int cr;
                for (cr = 0; cr < myPoints.DrawingPoints.Count; cr++)
                {
                    if (available[cr])
                        break;
                }

                result[u] = cr;

                for (int i = 0; i < myPoints.DrawingPoints.Count; i++)
                {
                    available[i] = true;
                }
            }

           

            for (int u = 0; u < myPoints.DrawingPoints.Count; u++)
            {
                myPoints.DrawingPoints[u].DrawingPen.Color = colors[result[u] % colors.Length];
                coloringResults[u] = result[u];
            }

            panel1.Invalidate();
            label2.Text = "Status: Colored";

        }

        //Draw
        private void panel1_MouseClick(object sender, MouseEventArgs e) {

            if (Colored) { ResetColors(); }

            if (AddPointMode) {

                MyPoints.DrawingPoint newPoint = new MyPoints.DrawingPoint();
                newPoint.Dot = new Rectangle(new Point(e.Location.X - 8, e.Location.Y - 8), new Size(16, 16));
                newPoint.DrawingPen = new Pen(Color.Black, 4);
                myPoints.DrawingPoints.Add(newPoint);
                adj[myPoints.DrawingPoints.Count - 1] = new List<int>();

                panel1.Invalidate();

                AddPointMode = false;
                label2.Text = "Status: Idle";

            } else if (RemovePointMode) {

                foreach (MyPoints.DrawingPoint mypoint in myPoints.DrawingPoints) {

                    if ((e.X > mypoint.Dot.X) && (e.X < mypoint.Dot.X + mypoint.Dot.Width) && (e.Y > mypoint.Dot.Y) && (e.Y < mypoint.Dot.Y + mypoint.Dot.Height)) {

                        mypoint.DeletionQueued = true;
                        panel1.Invalidate();

                        RemovePointMode = false;
                        label2.Text = "Status: Idle";

                        break;

                    }

                }

            } else if (AddConnectionMode) {

                for (int i = 0; i < myPoints.DrawingPoints.Count; i++) {

                    MyPoints.DrawingPoint mypoint = myPoints.DrawingPoints[i];

                    if ((e.X > mypoint.Dot.X) && (e.X < mypoint.Dot.X + mypoint.Dot.Width) && (e.Y > mypoint.Dot.Y) && (e.Y < mypoint.Dot.Y + mypoint.Dot.Height)) {

                        if (FirstSelectedPoint == null) {

                            FirstSelectedPoint = mypoint;
                            FirstSelectedPointIndex = i;
                            label2.Text = "Status: Click on second point to create connection";

                        } else {

                            if (FirstSelectedPoint != mypoint) {

                                SecondSelectedPoint = mypoint;
                                SecondSelectedPointIndex = i;
                                label2.Text = "Status: Idle";

                            }

                        }

                        break;

                    }

                }

                if (FirstSelectedPoint != null && SecondSelectedPoint != null) {

                    foreach (var record in ConnectedPoints) {

                        if ((record.Value.Item1 == FirstSelectedPoint && record.Value.Item2 == SecondSelectedPoint) || (record.Value.Item1 == SecondSelectedPoint && record.Value.Item2 == FirstSelectedPoint)) {
                            
                            AddConnectionMode = false;
                            break;

                        }

                    }

                    if (AddConnectionMode) {

                        MyLines.DrawingLine newLine = new MyLines.DrawingLine();
                        newLine.DrawingPen = new Pen(Color.Black, 4);
                        newLine.StartPoint = new Point(FirstSelectedPoint.Dot.Location.X + 8, FirstSelectedPoint.Dot.Location.Y + 8);
                        newLine.EndPoint = new Point(SecondSelectedPoint.Dot.Location.X + 8, SecondSelectedPoint.Dot.Location.Y + 8);

                        myLines.Add(newLine);
                        panel1.Invalidate();

                        AddConnectionMode = false;
                        ConnectedPoints.Add(newLine, (FirstSelectedPoint, SecondSelectedPoint));
                        adj[FirstSelectedPointIndex].Add(SecondSelectedPointIndex);
                        adj[SecondSelectedPointIndex].Add(FirstSelectedPointIndex);

                    } else {

                        FirstSelectedPoint = null;
                        SecondSelectedPoint = null;
                        label2.Text = "Status: Idle";

                    }

                }

            }

        }



        //Create Point
        private void button3_Click(object sender, EventArgs e) {

            if (Colored) { ResetColors(); }

            if (FirstSelectedPoint != null || SecondSelectedPoint != null) {
                FirstSelectedPoint = null;
                SecondSelectedPoint = null;
            }

            if (!AddPointMode) {

                AddPointMode = true;
                RemovePointMode = false;
                AddConnectionMode = false;

                label2.Text = "Status: Click on panel to create a point";

            } else {

                AddPointMode = false;
                label2.Text = "Status: Idle";

            }

        }

        private void Reset()
        {
            foreach (MyPoints.DrawingPoint mypoint in myPoints.DrawingPoints)
            {

                mypoint.DeletionQueued = true;
                panel1.Invalidate();

                RemovePointMode = false;
                label2.Text = "Status: Idle";

            }

            
        }

        private void ResetColors()
        {
            for (int u = 0; u < myPoints.DrawingPoints.Count; u++)
            {
                myPoints.DrawingPoints[u].DrawingPen.Color = Color.Black;
            }

            panel1.Invalidate();

            adj.Clear();
            FirstSelectedPointIndex = 0;
            SecondSelectedPointIndex = 0;
            if (Colored) { Colored = false; }

        }

        private void ResetGraph(object sender, EventArgs e) {

            Reset();
            ResetColors();

        }


        //Remove Point
        private void button5_Click(object sender, EventArgs e) {

            if (Colored) { ResetColors(); }

            if (FirstSelectedPoint != null || SecondSelectedPoint != null) {
                FirstSelectedPoint = null;
                SecondSelectedPoint = null;
            }

            if (!RemovePointMode) {
                AddPointMode = false;
                RemovePointMode = true;
                AddConnectionMode = false;

                label2.Text = "Status: Click on point to remove it";

            } else {

                RemovePointMode = false;
                label2.Text = "Status: Idle";

            }

        }



        //Color graph mode
        private void button4_Click_1(object sender, EventArgs e) {

            if (Colored) { ResetColors(); }

            if (myPoints.DrawingPoints.Count > 0) {

                string selectedAlgorithm = comboBox1.Text;

                if (selectedAlgorithm == "Greedy")
                {

                    // TODO: Greedy function
                    greedyColoring();
                    button4.Invalidate();

                }
                else if (selectedAlgorithm == "Backtrack (MRV)")
                {

                    // TODO: Backtrack function
                    backtrackMRVColoring();
                    button4.Invalidate();

                } else if (selectedAlgorithm == "Backtrack (Power)") {

                    backtrackPowerColoring();
                    button4.Invalidate();

                }

            }

            button4.Invalidate();

        }

        

        //Render
        private void panel1_Paint(object sender, PaintEventArgs e) {

            foreach (MyPoints.DrawingPoint mypoint in myPoints.DrawingPoints) {

                if (mypoint.DeletionQueued) {

                    foreach (var record in ConnectedPoints) {

                        if (record.Value.Item1 == mypoint || record.Value.Item2 == mypoint) {
                            record.Key.DeletionQueued = true;
                        }

                    }

                    mypoint.Delete();
                    continue;

                }

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(mypoint.DrawingPen, mypoint.Dot);

            }

            foreach (MyLines.DrawingLine myline in myLines.DrawingLines) {

                if (myline.DeletionQueued) {
                    myline.Delete();
                    continue;
                }

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(myline.DrawingPen, myline.StartPoint, myline.EndPoint);

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => {

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.Title = "Save Results";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (myPoints.DrawingPoints == null || myPoints.DrawingPoints.Count == 0)
                    {
                        MessageBox.Show("No points to save.");
                        return;
                    }

                    if (string.IsNullOrEmpty(saveFileDialog1.FileName))
                    {
                        MessageBox.Show("No file name specified.");
                        return;
                    }

                    using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                    {
                        foreach (var result in coloringResults)
                        {
                            writer.WriteLine("Vertex " + result.Key + " --->  Color " + result.Value);
                        }
                    }
                }

            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        //Add Connection
        private void button2_Click(object sender, EventArgs e) {

            if (FirstSelectedPoint != null || SecondSelectedPoint != null) {
                FirstSelectedPoint = null;
                SecondSelectedPoint = null;
            }

            if (!AddConnectionMode) {

                AddPointMode = false;
                RemovePointMode = false;
                AddConnectionMode = true;

                label2.Text = "Status: Click on first point";

            } else {

                AddConnectionMode = false;
                label2.Text = "Status: Idle";

            }

        }

    }

}