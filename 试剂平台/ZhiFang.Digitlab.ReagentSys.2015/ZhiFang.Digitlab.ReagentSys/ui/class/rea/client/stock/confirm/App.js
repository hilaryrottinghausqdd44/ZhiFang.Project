
/**
 * 入库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.confirm.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '验货单入库',
	border: false,	
	/**验货单ID*/
//  BmsCenSaleDocConfirmID:null,
    BmsCenSaleDocConfirmID:'5022491263190288619',
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**是否严格模式，严格1,混合模式’0*/
	IsStrictMode:'0',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.BmsCenSaleDocConfirmID){
			me.DocForm.isEdit(me.BmsCenSaleDocConfirmID);
			me.DtlGrid.onSearch();
			
		}
		//盒条码浮动窗体信息关闭
		me.on({
			close:function(){
				if(!me.DtlGrid.WinDtlPanel) return;
				me.DtlGrid.WinDtlPanel.close();
			}
		});
		me.DtlGrid.on({
			onStoreInClick:function(grid){
				me.onSaveClick();
			},
			nodata:function(){
				if(!me.DtlGrid.WinDtlPanel) return;
				me.DtlGrid.WinDtlPanel.close();
			}
		});
		me.DocForm.on({
			load:function(form,data){
				if(data && data.value){
					me.DtlGrid.ReaCompID=data.value.BmsCenSaleDocConfirm_ReaCompID;
				}
			}
		}); 
		
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocForm = Ext.create('Shell.class.rea.client.stock.confirm.DocForm', {
			title: '验收单入库',
			itemId: 'DocGrid',
			region: 'north',
			formtype:'add',
			header: false,
		    /**带功能按钮栏*/
	        hasButtontoolbar:false,
	        BmsCenSaleDocConfirmID:me.BmsCenSaleDocConfirmID,
			height: 100,
			border:false,
			split: false,
			collapsible: false,
			collapsed: false
		});
		
		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.confirm.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			IsStrictMode:me.IsStrictMode,
			BmsCenSaleDocConfirmID:me.BmsCenSaleDocConfirmID,
			 /**供应商ID*/
			ReaCompID:me.ReaCompID
		});
		var appInfos = [me.DocForm, me.DtlGrid];
		return appInfos;
	},
	clearData: function() {
		var me = this;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**入库保存服务*/
	onSaveClick:function(){
		var me=this;
		//验证验货单入库明细
		var isExect = me.DtlGrid.isVerification();
	    if(!isExect) return;
	    //获取本次入库合计总额
	    var count= me.DtlGrid.getSumTotal();
 		//获取总单信息
		var bmsindoc=me.DocForm.getAddParams();
		bmsindoc.TotalPrice=count;
		//验货单明细id
		var dtlDocConfirmIDStr=me.DtlGrid.getDtlConfirmID();
		//入库明细
		var ReaBmsInDtlVO=me.DtlGrid.getReaBmsInDtl(bmsindoc);
		var url = JShell.System.Path.getUrl(me.DtlGrid.addUrl);
		
		//	/**扫码模式(严格模式:strict,混合模式：mixing)*/
        var CodeScanningMode = "mixing";
        if(me.IsStrictMode=='1'){
        	CodeScanningMode='strict';
        }
		var params = Ext.JSON.encode({entity:bmsindoc,docConfirmID:me.BmsCenSaleDocConfirmID,dtlDocConfirmIDStr:dtlDocConfirmIDStr,dtAddList:ReaBmsInDtlVO,codeScanningMode:CodeScanningMode});		
		JShell.Server.post(url,params,function(data){
			me.hideMask();
			if(data.success){
				me.DtlGrid.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});