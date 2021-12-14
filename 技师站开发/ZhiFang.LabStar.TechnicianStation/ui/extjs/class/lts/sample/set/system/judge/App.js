/**
 * 智能审核设置
 * @author Jcall
 * @version 2020-06-30
 * @desc liangyl 2021-03-24 增加保存设置和采用默认值代码
 */
Ext.define('Shell.class.lts.sample.set.system.judge.App',{
	extend:'Ext.tab.Panel',
	requires:['Shell.ux.toolbar.Button'],
	title:'智能审核设置',
	//tabPosition:'bottom',
	activeTab:0,
	width:500,
	height:300,
	
	//获取常规检验参数分类列表
	getListUrl:'/ServerWCF/CommonService.svc/GetClassDic?classnamespace=ZhiFang.Entity.LabStar&classname=Para_NTestType',
    //保存当前页面设置
    saveUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemDefaultPara',
   	lastData:{},
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//获取智能审核设置枚举
		me.loadEnumData(function(){
			me.setActiveTab(0);
		});
	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = [{
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'bottomToolbar',
			items:['->',{
				iconCls:'button-config',text:'当前页签采用默认设置',tooltip:'当前页签采用默认设置',
		    	handler:function(but){me.onDefaultClick();}
			},'-',{
				iconCls:'button-save',text:'保存设置',tooltip:'保存设置',
		    	handler:function(but){me.onSaveClick();}
			}]
		}];
		me.callParent(arguments);
	},
	//加载智能审核设置枚举
	loadEnumData:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getListUrl;
	
		JShell.Server.get(url, function(data){
			if(data.success){
				me.createItems(data.value || []);
				callback();
			}else{
				me.update('<div style="padding:50px 10px;text-align:center;">' + data.msg + '</div>');
			}
		});
	},
	//创建内部页签
	createItems: function(list) {
		var me = this,
			len = list.length,
			items = me.startTab();//智能审核开始条件页签,仅显示];
		
		for(var i=0;i<len;i++){
			items.push(Ext.create('Shell.class.lts.sample.set.system.judge.Params',{
				title:list[i].Name,
				itemId:list[i].Code,
				TypeCode:list[i].Code
			}));
		}
		
		me.add(items);
	},
	
	//当前页签采用默认设置
	onDefaultClick:function(){
		var me = this;
		var comtab = me.getActiveTab(me.items.items[0]);
		comtab.loadFactoryData(); 
	}, 
	
	//保存设置
	onSaveClick:function(){
		var me = this,
		    url = JShell.System.Path.ROOT + me.saveUrl;
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode({entityList:me.getEntityList()}),function(data){
			if(data.success){
				me.fireEvent('save',me);
				var comtab = me.getActiveTab(me.items.items[0]);
                comtab.removeAll();
                comtab.loadEnumData();
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//保存设置封装
	getEntityList : function(){
		var me = this,
		    entityList=[]
		var comtab = me.getActiveTab(me.items.items[0]);
		for(var i=0;i<comtab.items.items.length;i++){
			entityList.push({
				Id:comtab.items.items[i].ParaID,
				ParaValue:comtab.items.items[i].value ? "1" : "0",
				OperatorID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
				Operator:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
				BVisible: 1,
				CName: comtab.items.items[i].boxLabel,
				IsUse:1,
				ParaEditInfo: comtab.items.items[i].ParaEditInfo,
				ParaNo: comtab.items.items[i].name,
				ParaType: comtab.items.items[i].ParaType,
				ShortCode: comtab.items.items[i].ShortCode,
				SystemCode: comtab.items.items[i].BPara_SystemCode,
				TypeCode:  comtab.items.items[i].BPara_TypeCode
			});
		}
		return entityList;
	},
	//智能审核开始条件页签,仅显示
	startTab : function(){
		var me = this,
		    items = [];
		items.push(Ext.create('Ext.panel.Panel',{
			title:'智能审核开始条件',itemId:'startTab',TypeCode:'',bodyPadding:'10px 10px 10px 15px',
			items:[{ 
				xtype: 'displayfield',labelSeparator: '',fieldLabel: '',value: '病人基本信息存在:姓名，性别'
			},{ 
				xtype: 'displayfield',labelSeparator: '',fieldLabel: '',value: '样本基本信息存在:样本类型'
			},{ 
				xtype: 'displayfield',labelSeparator: '',fieldLabel: '',value: '存在检验项目'
			},{ 
				xtype: 'displayfield',labelSeparator: '',fieldLabel: '',value: '项目检验完成:有报告值'
			}]
		}));
	    return items;
	}
});