/**
 * 结果图片
 * @author Jcall
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.Images', {
	extend:'Ext.panel.Panel',
	requires:[
		'Shell.ux.toolbar.Button'
	],
	title:'结果图片',
	width:200,
    height:250,
	bodyPadding:1,
	//布局方式
	layout:'anchor',
	//每个组件的默认属性
	defaults:{ 
		anchor:'100%'
	},
	
	//获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestGraphByHQL?isPlanish=true',
    //获取LIS图形库图形结果表数据
    getImageInfoUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisGraphData',
    //小组ID
    sectionId:null,
	//检验单数据
    testFormRecord:null,
    //是否只读
	isReadOnly:false,
	
	//检验图形字段
	//图形编号,图形名称,图形类型,图形图数据ID,图形数据说明,图形备注
	//图形高度,图形宽度,显示次序,是否报告
	FIELDS:[
		'GraphNo','GraphName','GraphType','GraphDataID','GraphInfo','GraphComment',
		'GraphHeight','GraphWidth','DispOrder','IsReport'
	],
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		
		me.callParent(arguments);
	},
	//创建挂靠功能栏
	createDockedItems:function(){
		var me = this;
		
		var dockedItems = [{
			xtype:'uxButtontoolbar',
			dock:'top',
			itemId: 'topToolbar',
			items: me.sectionId ? ['refresh', 'add'] : ['refresh']
		}];
		
		return dockedItems;
	},
	
	//点击新增按钮
	onAddClick:function(){
		var me = this;
		if (!me.testFormRecord) {
			JShell.Msg.alert("请选择一条检验单进行新增！");
			return;
		}
		JShell.Win.open('Shell.class.lts.sample.result.sample.AddImage',{
			sectionId:me.sectionId,
			testFormId:me.testFormRecord.get('LisTestForm_Id'),
			listeners:{
				save:function(p){
					p.close();
					me.onRefreshClick();
				}
			}
		}).show();
	},
	//点击刷新按钮
	onRefreshClick:function(){
		var me = this;
		me.onSearch(me.testFormRecord);
	},
	
	//查询
	onSearch:function(testFormRecord){
		var me = this;
		
		me.testFormRecord = testFormRecord;
		
		me.getLisTestGraphList(function(list){
			me.removeAll();
			me.update();
			me.createImageItems(list);
		});
	},
	//创建图形组件
	createImageItems:function(list){
		var me = this,
			len = list.length,
			items = [];
			
		for(var i=0;i<len;i++){
			items.push(me.createOneImagePanel(list[i]));
		}
		
		if(items.length > 0){
			me.add(items);
		}else{
			me.update('<div style="padding:10px;text-align:center;">没有找到图片！</div>');
		}
	},
	//和藏剑一个图形面板
	createOneImagePanel:function(info){
		var me = this;
		var panel = {
			xtype:'panel',width:100,height:110,margin:1,
			info:info,
			GraphDataID:info.LisTestGraph_GraphDataID,
			listeners:{
				afterrender:function(p){
					me.getImageInfo(0,info.LisTestGraph_GraphDataID,function(data){
						if (data.success) {
							p.add({
								xtype: 'image', width: '100%',height:"100%",
								src: data.value//JShell.System.Path.ROOT + me.getImageInfoUrl + '?graphSizeType=0&graphDataID=' + (p.GraphDataID ? p.GraphDataID : 0)
							});
						}else{
							p.update('<div style="padding:20px;text-align:center;">没有图片信息！</div>');
						}
					});
				},
				el:{
					dblclick:function(){
						if(info.LisTestGraph_GraphDataID){
							JShell.Win.open('Ext.panel.Panel',{
								title:'大图片',
								width:600,
								height: 300,
								autoScroll:true,
								GraphDataID:info.LisTestGraph_GraphDataID,
								listeners:{
									afterrender:function(p){
										me.getImageInfo(1,info.LisTestGraph_GraphDataID,function(data){
											if (data.success) {
												p.add({
													xtype: 'image', width: info.LisTestGraph_GraphWidth, height: info.LisTestGraph_GraphHeight,
													src: data.value//JShell.System.Path.ROOT + me.getImageInfoUrl + '?graphSizeType=0&graphDataID=' + (p.GraphDataID ? p.GraphDataID : 0)
												});
											}else{
												p.update('<div style="padding:20px;text-align:center;">没有图片信息！</div>');
											}
										});
									}
								}
							}).show();
						}
					}
				}
			}
		};
		
		return panel;
	},
	//获取检验图形列表
	getLisTestGraphList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
		
		url += '&fields=' + "LisTestGraph_" + me.FIELDS.join(",LisTestGraph_");
		url += '&where=listestgraph.LisTestForm.Id=' + me.testFormRecord.get('LisTestForm_Id');
		
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				me.update('<div style="padding:10px;color:red;">' + data.msg + '</div>');
			}
		});
	},
	//获取图形信息
	getImageInfo:function(graphSizeType,graphDataID,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getImageInfoUrl;
			
		url += '?graphSizeType=' + graphSizeType + '&graphDataID=' + (graphDataID ? graphDataID : 0);
		
		JShell.Server.get(url,function(data){
			callback(data);
		});
	}
});
