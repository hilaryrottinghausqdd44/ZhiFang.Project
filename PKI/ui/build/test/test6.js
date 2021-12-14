Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var ClassCode = Ext.define('QCMatApp',{
		extend:'Ext.panel.Panel',alias:'widget.QCMatApp',
		title:'应用',width:832,height:440,
		layout:{type:'border',regionWeights:{south:3}},
		getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
		appInfos:[{
			width:830,height:213,appId:'4630135670202725311',
			itemId:'QCMatList',title:'列表',region:'center',split:false,
			collapsible:false,collapsed:false,border:true
		},{
			y:213,width:830,height:200,appId:'5055941346518748653',
			itemId:'QCMatForm',title:'表单',region:'south',split:false,
			collapsible:false,collapsed:false,border:true
		}],
		comNum:0,
		afterRender:function(){
			var me=this;
			me.callParent(arguments);
			me.createItems();
		},
		createItems:function(){
			var me = this;
			var appInfos=me.getAppInfos();
			for(var i in appInfos){
				var id=appInfos[i].appId;
				var callback=me.getCallback(appInfos[i]);
				me.getAppInfoFromServer(id,callback);
			}
		},
		getCallback:function(appInfo){
			var me=this;
			var callback=function(obj){
				if(obj.success&&obj.appInfo!=''){
					var ModuleOperCode=obj.appInfo.BTDAppComponents_ModuleOperCode;
					var ClassCode=obj.appInfo.BTDAppComponents_ClassCode;
					var cl=eval(ClassCode);
					var callback2=function(panel){
						alert(appInfo.title);
						me.initLink();
					};
					appInfo.callback=callback2;
					var panel = Ext.create(cl,appInfo);
					me.add(panel);
				}else{
					appInfo.html=obj.ErrorInfo;
					var panel=Ext.create('Ext.panel.Panel',appInfo);
					me.add(panel);
				}
			};
			return callback;
		},
		getAppInfos:function(){
			var me=this;
			var appInfos=me.appInfos;
			for(var i in appInfos){
				if(appInfos[i].title==''){
					delete appInfos[i].title;
				}else if(appInfos[i].title=='_'){
					appInfos[i].title='';
				}
			}
			return Ext.clone(appInfos);
		},
		initLink:function(){
			var me=this;
			var appInfos=me.getAppInfos();
			var length=appInfos.length;
			me.comNum++;
			if(me.comNum==length){
				var _QCMatList=me.getComponent('QCMatList');
				_QCMatList.on({
					itemclick:function(view,record){
						var id=record.get(_QCMatList.objectName+'_Id');
						var _QCMatForm=me.getComponent('QCMatForm');
						_QCMatForm.load(id);
					}
				});
				var _QCMatList=me.getComponent('QCMatList');
				_QCMatList.on({
					addClick:function(but){
						var _QCMatForm=me.getComponent('QCMatForm');
						_QCMatForm.isAdd();
					}
				});
				var _QCMatList=me.getComponent('QCMatList');
				_QCMatList.on({
					editClick:function(but){
						var list=_QCMatList;
						var records=list.getSelectionModel().getSelection();
						if(records.length==1){
							var record=records[0];
							var id=record.get(_QCMatList.objectName+'_Id');
							var _QCMatForm=me.getComponent('QCMatForm');
							_QCMatForm.isEdit(id);
						}else{
							Ext.Msg.alert('提示','请选择一条数据进行操作！');
						}
					}
				});
				var _QCMatList=me.getComponent('QCMatList');
				_QCMatList.on({
					showClick:function(but){
						var list=_QCMatList;
						var records=list.getSelectionModel().getSelection();
						if(records.length==1){
							var record=records[0];
							var id=record.get(_QCMatList.objectName+'_Id');
							var _QCMatForm=me.getComponent('QCMatForm');
							_QCMatForm.isShow(id);
						}else{
							Ext.Msg.alert('提示','请选择一条数据进行操作！');
						}
					}
				});;
				if(Ext.typeOf(me.callback)=='function'){
					me.callback(me);
				}
			}
		},
		getAppInfoFromServer:function(id,callback){
			var me=this;var url=me.getAppInfoServerUrl+'?isPlanish=true&id='+id;
			Ext.Ajax.defaultPostHeader='application/json';
			Ext.Ajax.request({
				async:false,url:url,method:'GET',timeout:2000,
				success:function(response,opts){
					var result=Ext.JSON.decode(response.responseText);
					if(result.success){
						var appInfo='';
						if(result.ResultDataValue&&result.ResultDataValue!=''){
							appInfo=Ext.JSON.decode(result.ResultDataValue);
						}
						if(Ext.typeOf(callback)=='function'){
							var obj={success:false,ErrorInfo:'没有获取到应用组件信息!'};
							if(appInfo!=''){
								obj={success:true,appInfo:appInfo};
							}
							callback(obj);
						}
					}else{
						if(Ext.typeOf(callback)=='function'){
							var obj={success:false,ErrorInfo:'获取应用组件信息失败！错误信息【<b style=\"color:red\">'+result.ErrorInfo+'</b>】'};
							callback(obj);
						}
					}
				},
				failure:function(response,options){
					if(Ext.typeOf(callback)=='function'){
						var obj={success:false,ErrorInfo:'获取应用组件信息请求失败！'};
						callback(obj);
					}
				}
			});
		}
	});
	
	var panel = Ext.create(ClassCode,{
//		autoScroll:true,
//		modal:false,//模态
//		floating:true,//漂浮
//		closable:true,//有关闭按钮
//		resizable:true,//可变大小
//		draggable:true//可移动
	});
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});