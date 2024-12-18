using System;
using System.Reflection;
using System.Windows.Forms;

namespace IconsExtractorGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    Assembly assembly = Assembly.LoadFrom("IconsExtractor.exe");

                    Type myType = assembly.GetType("Program");



                    object instance = Activator.CreateInstance(myType);

                    var members = myType.GetMembers(BindingFlags.Instance | BindingFlags.Public);
                    var constructors = myType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                    var events = myType.GetEvents(BindingFlags.Instance | BindingFlags.Public);
                    var fields = myType.GetFields(BindingFlags.Instance | BindingFlags.Public);
                    var geericrguments = myType.GetGenericArguments();
                    var interfaces = myType.GetInterfaces();
                    var methods = myType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                    var nestedTypes = myType.GetNestedTypes(BindingFlags.Instance | BindingFlags.Public);
                    var properties = myType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    var runtimeEvents = myType.GetRuntimeEvents();
                    var runtimeFields = myType.GetRuntimeFields();
                    var runtimeMethods = myType.GetRuntimeMethods();




                    MethodInfo methodInfo = myType.GetMethod("Main");

                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    var length = parameters.Length;

                    //Usage: IconsExtractor.exe <filePath> <outputPath>
                    string filePath = textBox1.Text;
                    string outputPath = textBox2.Text;
                    string[] args = new string[] { filePath, outputPath };
                    object[] objArgs = new object[] { args };

                    methodInfo.Invoke(instance, objArgs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.SelectedPath;
            }
        }
    }
}
