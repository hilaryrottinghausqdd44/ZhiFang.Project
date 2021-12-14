/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-12
 */
Ext.define('Shell.class.weixin.sampling.basic.apply.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'新增采样',	
	//是否加载过数据
	hasLoaded:false,
	account:'',
	layout: 'border',
	testItemDetails:'',
	NREQUESTFORM_ADD_OR_UPDATE_URL:JShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/SaveOSConsumerUserOrderForm",
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.wGrid.on({
		    /* itemclick: function (v, record) {
				var newArr = me.testItemDetails.filter(item => item.SItemNo==record.data.ItemNo);
		        console.log(record);
		        JShell.Action.delay(function () {
					me.cGrid.store.removeAll();
					me.cGrid.store.add(newArr);
					me.cGrid.enableControl();
					
					me.eGrid.testItemDetails=newArr;
					me.eGrid.storeDataDispose();
				},null,500);
			},
            select: function (RowModel, record) {
				var newArr = me.testItemDetails.filter(item => item.SItemNo==record.data.ItemNo);				
				console.log(newArr);
                JShell.Action.delay(function () {
					me.cGrid.store.removeAll();
					me.cGrid.store.add(newArr);
					me.cGrid.enableControl();
					
					me.eGrid.testItemDetails=newArr;
					me.eGrid.storeDataDispose();
                },null,500);
			} */
		});
		me.eGrid.on({
			DataSave:function(BarCodeList){
				var comboGridStore = me.wGrid.store.getRange(),
				    itemGridStore = me.cGrid.store.getRange(),
				    CombiItems = [],
					jsonentity ={};
				me.showMask("数据保存中请稍等！");
				//拼接项目信息
				for (var citem of comboGridStore) {					
					var CombiItem = {};
					CombiItem["CombiItemName"] = citem.data.Name;
					CombiItem["CombiItemNo"] = citem.data.ItemNo;
					var CombiItemDetailList = [];
					for(var iitem of itemGridStore){
						if(citem.data.ItemNo == iitem.data.SItemNo){
							var CombiItemDetailentity = {};
							CombiItemDetailentity["CombiItemDetailName"] = iitem.data.CName;
							CombiItemDetailentity["CombiItemDetailNo"] = iitem.data.ItemNo;
							CombiItemDetailList.push(CombiItemDetailentity);
						}
					}
					CombiItem["CombiItemDetailList"] = CombiItemDetailList;
					CombiItems.push(CombiItem);
				}
				
				var NrequestForm = me.form.getSaveData();				
				jsonentity["BarCodeList"] = BarCodeList;
				jsonentity["CombiItems"] = CombiItems;
				jsonentity["NrequestForm"] = NrequestForm;
				jsonentity["PayCode"] = me.form.getComponent("PayCode").getValue();
				jsonentity["flag"] = 1;
				var params = Ext.JSON.encode({jsonentity: jsonentity});
				JShell.Server.post(me.NREQUESTFORM_ADD_OR_UPDATE_URL,params,function(data){
					if(data.success){
						me.hideMask();
						me.form.setLastBarcodeInfo({});//清空Local数据
						 //成功提示，关闭提示框时清空消费单数据
						 JcallShell.Msg.confirm({title:'',msg:"申请单保存成功！"},function(){
							me.form.clearConsumeInfo();//清空与患者相关数据
							me.form.getComponent("PayCode").setValue("");//消费码
							me.wGrid.store.removeAll();//清空套餐列表
							me.wGrid.disableControl();
							me.cGrid.store.removeAll();//清空明细项目列表
							me.cGrid.disableControl();
							me.eGrid.store.removeAll();//清空条码列表
							me.eGrid.disableControl();
						});
						
					}else{
						me.hideMask();	
						JShell.Msg.error("保存申请单信息失败！错误信息：" + data.msg);
					}
				});	
			}
		})
	},
	initComponent:function(){
		var me = this;
		me.account = JShell.System.Cookie.get("000301");		
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	//创建内部组件
	createItems: function () {
		var me = this;
		me.form = Ext.create('Shell.class.weixin.sampling.basic.apply.Form', {
			region: 'north', itemId: 'form',height:130,account:me.account,
			header: false, border: false,
			autoScroll: true, split: true,
			collapsible: false, animCollapse: false,
			listeners:{
				onMaskShow:function(msg){
					me.showMask(msg);
				},
				onMaskHide:function(){
					me.hideMask();
				},
				changetable:function(UserOrderItem,testItemDetails){
					me.testItemDetails = testItemDetails;
					me.wGrid.store.removeAll();
					me.wGrid.store.add(UserOrderItem);
					me.wGrid.enableControl();
					
					me.cGrid.store.removeAll();
					me.cGrid.store.add(testItemDetails);
					me.cGrid.enableControl();
					
					me.eGrid.testItemDetails=testItemDetails;
					me.eGrid.storeDataDispose();					
				}
			} 
			
		});
		 me.wGrid = Ext.create('Shell.class.weixin.sampling.basic.apply.comboGrid', {
			region: 'west', itemId: 'wGrid', title:'订单套餐',
			header: true, border: false,width:200,
			autoScroll: true, split: true,
			collapsible: true,animCollapse: false
		});
		me.cGrid = Ext.create('Shell.class.weixin.sampling.basic.apply.ItemGrid', {
			region: 'center', itemId: 'cGrid', title:'明细项目',
			header: true, border: false,
			autoScroll: true, split: true,
			collapsible: false,animCollapse: false
		});
		me.eGrid = Ext.create('Shell.class.weixin.sampling.basic.apply.serialGrid', {
			region: 'east', itemId: 'eGrid', title:'条码列表',
			header: true, border: false,width:280,
			autoScroll: true, split: true,
			collapsible: true,animCollapse: false
		});
 
		return [me.form, me.wGrid,me.cGrid,me.eGrid];
	}
	
	
	
});