//Ext.onReady(function(){	
//	Ext.QuickTips.init();//初始化后就会激活提示功能
//	Ext.Loader.setConfig({enabled: true});//允许动态加载
//	
//	var str1 = "({xtype:'panel',width:300,height:100,title:'eval测试1'})";
//	
//	var str2 = "Ext.define('Ext.am.MyPanel',{extend:'Ext.panel.Panel',width:300,height:100,title:'eval测试2'});";
//	
//	var panel1 = eval(str1);
//	var panel2 = eval(str2);
//	panel2 = Ext.create(panel2);
//	//总体布局
//	var viewport = Ext.create('Ext.container.Viewport',{
//		//layout:'fit',
//		items:[panel1,panel2]
//	});
//});

function User(){
	var info = {
		name:'',
		age:0
	};
	
	function getInfo(){
		return info;
	};
	function setInfo(con){
		
		
		
		info = con;
	};
	
	function getName(){
		return info.name;
	};
	function setName(name){
		info.name = name;
	};
	
	function getAge(){
		return info.age;
	};
	function setAge(age){
		info.age = age;
	};
	
	function getAllInfo(){
		var str = "信息:" + info.name + info.age;
		return str;
	};
	
	return {
		getInfo:getInfo,
		setInfo:setInfo,
		getName:getName,
		setName:setName,
		getAge:getAge,
		setAge:setAge
	}
}

var user = User();

var config = {
	name:'张三',
	age:21
};

user.setInfo(config);
var name = user.getName();
var age = user.getAge();
var a = 1;

var name = "The Wondow";
var object = {
	name:'My Object',
	getName:function(){
		return function(){
			return this.name;
		}
	}
};
alert(object.getName()());











