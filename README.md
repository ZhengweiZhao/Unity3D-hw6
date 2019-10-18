# 3D Game Programming & Design：物理系统与碰撞
## 作业要求
1、改进飞碟（Hit UFO）游戏：

游戏内容要求：
按 adapter模式 设计图修改飞碟游戏
使它同时支持物理运动与运动学（变换）运动

## 代码分析
本次作业改进的要求是增加一个支持物理运动的功能。首先我们来了解一下游戏中的物理运动是如何体现的。
>在游戏物理引擎中，主要是刚体动力学。
>主要包括质点系动力学的基本定理，由动量定理、动量矩定理、动能定理以及由这三个基本定理推导出来的其他一些定理。动量、动量矩和动能（见能）是描述质点、质点系和刚体运动的基本物理量。
>- 考虑外部力对物体运动的影响
>- 包括重力、阻力、摩擦力等，以及物体的重量和形状，甚至弹性物体
>- 通常将一个物体当作刚体
>- 模拟物体在现实世界中的运动

本次作业难度还是比较大的，所以在代码结构上[参考](https://blog.csdn.net/qq_36297981/article/details/80072648)了前辈的博客。
因为是在上次的基础改进设计，所以我们使用了适配器模式:
>适配器模式（Adapter Pattern）是作为两个不兼容的接口之间的桥梁。这种类型的设计模式属于结构型模式，它结合了两个独立接口的功能。这种模式涉及到一个单一的类，该类负责加入独立的或不兼容的接口功能。这种模式的好处是:
1、可以让任何两个没有关联的类一起运行。 
2、提高了类的复用。
 3、增加了类的透明度。
  4、灵活性好。

设计的UML图如下：
![在这里插入图片描述](https://img-blog.csdnimg.cn/2019101811451664.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)
本次作业主要涉及的主要类中大部分和上次的一样，这里只对做了修改的部分和增加的代码部分进行一个分析说明：

为了使用适配器模式，加了一个IActionMananager的结构，这个函数的作用在于设定飞碟的发射轨迹，只要在这个函数中获取飞碟的刚体并为之添加一个随机方向的力就可以实现飞碟的物理学飞行：
```javascript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionManager{
	void addRandomAction (GameObject gameObj);
}
```
动力学的动作管理器 PhysisManager：
```javascript
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.game;

public class PhysisManager : SSActionManager, ISSActionCallback, IActionManager
{
	public RoundController scene;
	public MoveToAction action1, action2;
	public SequenceAction saction;
	float speed;
	public int If_Active;


	public void addRandomAction(GameObject gameObj)
	{
		Vector3 force = new Vector3 (
			UnityEngine.Random.Range(-10, 10),
			UnityEngine.Random.Range(-10, 10),
			UnityEngine.Random.Range(-10, 10)
		);
		int[] X = { -20, 20 };
		int[] Y = { -5, 5 };
		int[] Z = { -20, -20 };

		// 随机生成起始点和终点
		Vector3 starttPos = new Vector3(
			UnityEngine.Random.Range(-70, 70),
			UnityEngine.Random.Range(-10, 10),
			UnityEngine.Random.Range(100, 150)
		);

		gameObj.transform.position = starttPos;

		Vector3 randomTarget = new Vector3(
			X[UnityEngine.Random.Range(0, 2)],
			Y[UnityEngine.Random.Range(0, 2)],
			Z[UnityEngine.Random.Range(0, 2)]
		);
		gameObj.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObj.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);
		MoveToAction action = MoveToAction.getAction(randomTarget, gameObj.GetComponent<DiskData>().speed);

		RunAction(gameObj, action, this);
	}

	protected  void Start()
	{
		scene = (RoundController)SSDirector.getInstance().currentScenceController;
		scene.physisManager = this;
	}

	protected new void Update()
	{
		base.Update();
	}

	public void actionDone(SSAction source)
	{
		Debug.Log("Done");
	}
}

```
然后将这个script添加到main对象上。
此外，我们还需要在场景控制器中为这个类添加一个实例：
```javascript
diskFactory = Singleton<DiskFactory>.Instance;
scoreRecorder = Singleton<ScoreRecorder>.Instance;
actionManager = Singleton<RoundActionManager>.Instance;
physisManager = Singleton<PhysisManager>.Instance;
```
- 其他部分就不贴代码了，见GitHub。

## 游戏界面效果
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191018115444117.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191018115454763.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191018115505209.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191018115514939.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)
- 最后

[视频链接-lose🔗](https://pan.baidu.com/s/1D19_ZbLBL0JgTWjWuyJkyQ&shfl=sharepset)

[视频链接-win🔗](https://pan.baidu.com/s/1d6HWlyeKXLZT2BoGGEg18g&shfl=sharepset)


