using MPay4399Wrapper.Unisdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MPayNameSpace;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace MPay4399Wrapper
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Class : IClass
    {
        /*
         * 实现 4399MPay 调用  与NetAssMPay调用流程一致  只是换了几个类库罢了
         * 
         * 窃格瓦拉 - 赵明宇  
         * （题外话：本来我没做混淆就是为了给你们作参考。结果赵大神居然
         * 这么明目张胆的说要抄，可真是太厉害拉！）
         * Made by MonoTeam. 部分代码参考 NeteaseLogin模块.
         *  
         */

private MPaySdkManager MPaySDK = new MPaySdkManager();
private MPay MPay = new MPay();

public string Init4399MPay()
{
    this.MPaySDK.Init();
    this.MPaySDK.Login();
    return this.MPay.GetSAuthPropStr();
}

}

public interface IClass
{
string Init4399MPay();

}

namespace Unisdk
{
public class MPay : CppCliUnisdkMPay
{
    protected override void onCheckOrderFinish(int errorCode, int orderStatus, string productId, uint productCount, string orderId, string errReason)
    {
        throw new NotImplementedException();
    }

    protected override void onCompactViewClosed(int code)
    {
        throw new NotImplementedException();
    }

     protected override void onExtendFuncFinish(string json)
    {
        throw new NotImplementedException();
    }
    protected override void onLog(string log)
    {
        throw new NotImplementedException();
    }

    protected override void onInitFinish(int code)
    {
        Console.WriteLine("[MPay][onInitFinish]" + code.ToString());
        if (code != 0)
        {
            MessageBox.Show("Err onInitFinish\n登录账号失败，请尝试更换账号后重试。");
        }
    }

    protected override void onLoginFinish(int code)
    {
        Console.WriteLine("[MPay][onLoginFinish]" + code.ToString());

        if (code == 0)
        {
            Console.WriteLine("[MPay][SauthStatus]" + code.ToString());
            Console.WriteLine("[MPay][Sauth]" + base.GetSAuthPropStr());
        }
        else
        {
            if (code != 1)
            {
                MessageBox.Show("Err onLoginFinish\n登录账号失败，请尝试更换账号后重试。");
            }
        }
    }

    protected override void onLogoutFinish(int code)
    {
        bool flag = code != 0;
        if (!flag)
        {
            base.Login();
        }
    }


}
}
public class MPaySdkManager
{
public void Init()
{
    bool flag = this.mpay != null;
    if (!flag)
    {
        this.mpay = new MPay();
        this.mpay.Init("我的世界启动器", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Netease\\MCLauncher\\config\\mpay\\");
    }
}
public void Login()
{
    bool flag = this.mpay == null;
    if (!flag)
    {
        this.mpay.Login();
    }
}

public void Logout()
{
    bool flag = this.mpay == null;
    if (!flag)
    {
        this.mpay.Logout();
    }
}

public void RunLoop(float deltaTime)
{
    bool flag = this.mpay == null;
    if (!flag)
    {
        this.mpay.RunLoop(deltaTime);
    }
}

public void clean()
{
    bool flag = this.mpay == null;
    if (!flag)
    {
        this.mpay.Clean();
        this.mpay = null;
    }
}

public MPay mpay = null;
public string sauthJson = "";
}
}
