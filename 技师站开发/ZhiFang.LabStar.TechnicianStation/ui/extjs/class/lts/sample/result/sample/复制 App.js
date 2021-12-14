/**
 * 检验结果
 * @author Jcall
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.App', {
	extend: 'Shell.ux.panel.AppPanel',
	requires:[
		'Shell.ux.toolbar.Button'
	],
	title:'检验结果',
	
	//项目列表服务
	GET_ITEM_LIST_URL:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestItemByHQL?isPlanish=true',
	
	//每列最大行数
	MAX_NUMBER:10,
	//列数
	COL_NUMBER:1,
	//卡片宽度
	CARD_WIDTH:370,
	//卡片高度
	CARD_HEIGHT:25,
	//获取的项目集合
	ItemListData:[],
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建挂靠功能栏
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'bottomToolbar',
			items:[{
				xtype:'radiogroup',fieldLabel:'',
				columns:2,width:205,vertical:true,
				items:[
					{boxLabel:'所有检验项目',name:'rb',inputValue:'1',checked:true},
					{boxLabel:'按医嘱项目',name:'rb',inputValue:'2'}
				]
			},'-','add','del','-',{
				xtype:'radiogroup',fieldLabel:'',
				columns:2,width:100,vertical:true,
				itemId:'SelectModel',
				items:[
					{boxLabel:'列表',name:'rbSelectModel',inputValue:1,checked:true},
					{boxLabel:'表格',name:'rbSelectModel',inputValue:2}
				],
				listeners:{
					change:function(com,newValue,oldValue,eOpts){
						if(newValue.rbSelectModel == 1){//表格
							me.createGridPanel();
						}else{//列表
							me.createTablePanel();
						}
						me.onSearch();
						me.IsShowConfig(newValue.rbSelectModel);
					}
				}
			},{
				text:'设置',iconCls:'button-config',
				itemId:'config',hidden:true,
				handler:function(){
					me.onSetConfig();
				}
			}]
		}];
		return dockedItems;
	},
	//创建内部组件
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Ext.panel.Panel',{
			region:'center',itemId:'Grid',
			header:false,border:false,
			autoScroll:true,bodyPadding:1,
			items:[
				Ext.create('Shell.class.lts.sample.result.sample.Grid',{hidden:true}),
				Ext.create('Shell.class.lts.sample.result.sample.Table',{hidden:true})
			]
		});
		me.Images = Ext.create('Shell.class.lts.sample.result.sample.Images',{
			region:'east',itemId:'Images',width:155,
			header:false,border:false,
			autoScroll:true,split:true,
			collapsible:true//,collapseMode:'mini'
		});

		return [me.Grid,me.Images];
	},
	//根据项目ID变更数据
	onChangeInfo:function(itemId){
		var me = this,
			url = JShell.System.Path.ROOT + me.GET_ITEM_LIST_URL;
			
		url += '?where=' + where;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.ItemListData = data.value.list;
				var showMode = me.getShowMode();
				if(showMode == 1){//列表
					me.createListPanel(data.value.list);
				}else{//表格
					me.createTablePanel();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
		me.Images.onSearch(itemId);
	},
	
	
	//创建表格面板
	createGridPanel:function(){
		
	},
	//创建列表面板
	createTablePanel:function(){
		var me = this,
			list = me.ItemListData || [],
			len = list.length,
			items = [];
		
		for(var i=0;i<len;i++){
			items.push({
				xtype:'panel',margin:1,
				width:me.CARD_WIDTH,
				height:me.CARD_HEIGHT,
				html:me.createCell(list[i],i)
			});
		}
		me.onLayoutChangeByItems(items);
	},
	createCell:function(data,index){
		var me = this,		
			html = me.getCellTemplet();
			
		for(var i in data){
			var regExp = new RegExp("{" + i + "}","g");
			html = html.replace(regExp,data[i]);
		}
		html = html.replace(/{NO}/g,index+1);
	    html = me.getConfigCell(html,data);
		if(data.StatusName){
			var isH = data.StatusName.slice(0,1) == 'H';
			var isL = data.StatusName.slice(0,1) == 'L';
			var color = isH ? 'red' : isL ? 'blue' : '';
			var StatusImage = data.StatusName.replace(/H/g,'↑').replace(/L/g,'↓');
			if(color){
				html = html.replace(/{ItemSNameStyle}/g,'color:' + color);
				html = html.replace(/{StatusStyle}/g,'color:#ffffff;background-color:' + color);
				html = html.replace(/{StatusImage}/g,'<span style="color:' + color +'">' + StatusImage + '</span>');
				html = html.replace(/{TestResult}/g,'font-weight:bold;');
			}else{
				html = html.replace(/{ItemSNameStyle}/g,'');
				html = html.replace(/{StatusStyle}/g,'');
				html = html.replace(/{StatusImage}/g,'');
				html = html.replace(/{TestResult}/g,'');
			}
		}
		return html;
	},
	
	
	
	//返回显示模式
	getShowMode:function(){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
		    SelectModel = bottomToolbar.getComponent('SelectModel');
		    
		return SelectModel.getValue().rbSelectModel;
	}
});