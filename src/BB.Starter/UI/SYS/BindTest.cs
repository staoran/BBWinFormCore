using System.ComponentModel;
using BB.BaseUI.Extension;
using Furion.JsonSerialization;

namespace BB.Starter.UI.SYS;

public partial class BindTest : Form
{
    public BindTest()
    {
        InitializeComponent();
    }
 
    Entity obj = new Entity();
    private void Form1_Load(object? sender, EventArgs e)
    {
        // textBox1.DataBindings.Add("Text", obj, "Field"); 
        DataBinderTools.BindingTextEditBase(textBox1, obj, "Field");
        numericUpDown1.DataBindings.Add("Value", obj, "Decimal", true);
        checkBox1.DataBindings.Add("Checked", obj, "Bool", true);
    }
 
    private void button1_Click(object? sender, EventArgs e)
    {
        var l = new List<Entity> { obj, obj, obj };
        var bl = new BindingList<Entity>(l);
        var bls = JSON.Serialize(bl);
        var ls = JSON.Serialize(l);
        MessageBox.Show(obj.Field);
        MessageBox.Show(ls);
    }
 
    private void button2_Click(object? sender, EventArgs e)
    {
        obj.Field = DateTime.Now.ToString("HH:mm:ss,fff");
    }
 
    private void button3_Click(object? sender, EventArgs e)
    {
        MessageBox.Show(obj.Decimal.ToString());
    }
 
    private void button4_Click(object? sender, EventArgs e)
    {
        obj.Decimal += 1;
    }
 
    private void button5_Click(object? sender, EventArgs e)
    {
        MessageBox.Show(obj.Bool.ToString());
    }
 
    private void button6_Click(object? sender, EventArgs e)
    {
        obj.Bool = !obj.Bool;
    }
}
    
public class Entity:INotifyPropertyChanged
{
    private string m_Filed = "DefaultValue";
    public string Field
    {
        get => m_Filed;
        set
        {
            m_Filed = value;
            SendChangeInfo("Field");
        }
    }
 
    private void SendChangeInfo(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
 
    private decimal m_Decimal = (decimal)60.01;
    public decimal Decimal
    {
        get => m_Decimal;
        set
        {
            m_Decimal = value;
            SendChangeInfo("Decimal");
        }
    }
 
    private bool m_Bool = false;
    public bool Bool
    {
        get => m_Bool;
        set
        {
            m_Bool = value;
            SendChangeInfo("Bool");
        }
    }
 
    public event PropertyChangedEventHandler? PropertyChanged;
}