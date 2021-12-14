/**
 * 供货验收-实验室专用
 * @author Jcall
 * @version 2017-07-28
 */
Ext.define('Shell.class.rea.sale.lab.checkall.App', {
	extend: 'Ext.panel.Panel',
	title: '供货验收-实验室专用',

	layout:'border',
    bodyPadding:1,
    
    /**验收服务*/
    checkUrl:'/ReagentService.svc/RS_UDTO_ConfirmSaleByDocID',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//主单监听
		me.DocGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.onDocGridSelect(record);
				},null,200);
			},
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					me.onDocGridSelect(record);
				},null,200);
			},
			nodata:function(){
				me.DocForm.clearData();
				me.DtlGrid.clearData();
				me.DtlInfo.clearData();
			},
			checkclick:function(p,record){
				me.onCheckClick(record);
			}
		});
		//明细监听
		me.DtlGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.onDtlGridSelect(record);
				},null,200);
			},
			itemclick:function(view,record){
				JShell.Action.delay(function(){
					me.onDtlGridSelect(record);
				},null,200);
			},
			nodata:function(){
				me.DtlInfo.clearData();
			}
		});
		
		setTimeout(function(){
			me.DocGrid.focus();
		},2000);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
			
		//供货单状态：0=临时；2=已审核；1=已验收；
		//供货单的实验室=本机构//and (供货单状态=2)
		var cenOrgId = JShell.REA.System.CENORG_ID;
		var defaultWhere = 'bmscensaledoc.Lab.Id=' + cenOrgId;
		
		//主单列表
		me.DocGrid = Ext.create('Shell.class.rea.sale.lab.checkall.DocGrid',{
			region:'center',itemId:'DocGrid',header:false,defaultLoad:true,
			defaultWhere:defaultWhere
		});
		//主单内容
		me.DocForm = Ext.create('Shell.class.rea.sale.basic.DocForm',{
			region:'east',itemId:'DocForm',header:false,
			split:true,collapsible:true
		});
		//明细列表
		me.DtlGrid = Ext.create('Shell.class.rea.sale.lab.checkall.DtlGrid',{
			region:'center',itemId:'DtlGrid',header:false
		});
		//明细内容
		me.DtlInfo = Ext.create('Shell.class.rea.sale.basic.DtlInfo',{
			region:'east',itemId:'DtlInfo',header:false,
			split:true,collapsible:true
		});
		
		items.push({
			region:'north',header:false,
			split:true,collapsible:true,
			itemId:'DocPanel',height:300,
			layout:'border',border:false,
			items:[me.DocGrid,me.DocForm]
		});
		items.push({
			region:'center',header:false,border:false,
			itemId:'DtlPanel',layout:'border',
			items:[me.DtlGrid,me.DtlInfo]
		});
		
		return items;
	},
	
	/**主单选中触发*/
	onDocGridSelect:function(record){
		var me = this;
		
		var id = record.get(me.DocGrid.PKField);
		var Status = record.get('BmsCenSaleDoc_Status') + '';
		
		//显示主单内容
		me.DocForm.isShow(id);
		//清空明细数据
		me.DtlInfo.clearData();
		//加载明细数据
		me.DtlGrid.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + id;
		if(Status == '2'){//供应商已审核
			me.DtlGrid.onSearch();
		}else{
			me.DtlGrid.onSearchOnlyRead();//只查看
		}
	},
	/**明细选中触发*/
	onDtlGridSelect:function(record){
		var me = this;
		
		me.DtlInfo.initData({
			CName:record.get('BmsCenSaleDtl_GoodsName'),
			EName:record.get('BmsCenSaleDtl_Goods_EName'),
			Unit:record.get('BmsCenSaleDtl_GoodsUnit'),
			UnitMemo:record.get('BmsCenSaleDtl_UnitMemo'),
			LotNo:record.get('BmsCenSaleDtl_LotNo'),
			InvalidDate:JShell.Date.toString(record.get('BmsCenSaleDtl_InvalidDate'),true),
			Count:record.get('BmsCenSaleDtl_GoodsQty'),
			Price:record.get('BmsCenSaleDtl_Price')
		});
	},
	
	/**验收处理*/
	onCheckClick:function(record){
		var me = this;
		
		//明细是否存在异常
		var errorMsg = me.DtlGrid.getDataErrorMsg();
		if(errorMsg){
			JShell.Msg.error(errorMsg);
			return;
		}
		
		//验收
		me.onCheckData(record);
	},
	onCheckData:function(record){
		var me = this,
			changeDtls = me.DtlGrid.store.getModifiedRecords(),//获取修改过的行记录
			len = changeDtls.length,
			isError = false;
			
		if(len > 0) isError = true;
		
		JShell.Win.open('Shell.class.rea.sale.lab.check.CheckForm',{
			resizable:false,
			isError:isError,
			listeners:{
				accept:function(p,data){
					me.onSaveCheckData(p,data,record);
				}
			}
		}).show();
	},
	onSaveCheckData:function(p,data,record){
		var me = this,
			url = JShell.System.Path.ROOT + me.checkUrl;
			
		//Post:'docID,invoiceNo,accepterMemo,secAccepterAccount,secAccepterPwd,listBmsCenSaleDtl,secAccepterType'
		//secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)
		var params = {
			docID:record.get(me.DocGrid.PKField),
			invoiceNo:data.InvoiceNo,
			accepterMemo:data.AccepterMemo,
			secAccepterAccount:data.Account,
			secAccepterPwd:data.Pwd,
			secAccepterType:3
		};
		
		var changeDtls = me.DtlGrid.store.getModifiedRecords(),//获取修改过的行记录
			len = changeDtls.length,
			listBmsCenSaleDtl = [];
			
		for(var i=0;i<len;i++){
			var rec = changeDtls[i];
			listBmsCenSaleDtl.push({
				Id:rec.get(me.DtlGrid.PKField),
				AcceptCount:rec.get('BmsCenSaleDtl_AcceptCount'),
				AccepterErrorMsg:rec.get('BmsCenSaleDtl_AccepterErrorMsg')
			});
		}
		
		if(listBmsCenSaleDtl.length > 0){
			params.listBmsCenSaleDtl = listBmsCenSaleDtl;
		}
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				p.close();
				JShell.Msg.alert('验收成功！',null,1000);
				me.DocGrid.onSearch();
				me.onOpenShowApp(record);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**打开查询页面*/
	onOpenShowApp:function(record){
		var me = this;
		var id = record.get(me.DocGrid.PKField);
		var where = 'bmscensaledoc.Id=' + id;
		
		var maxHeight = document.body.clientHeight - 20;
		var maxWidth = document.body.clientWidth - 20;
		
		JShell.Win.open('Shell.class.rea.sale.lab.show.App',{
			resizable:true,
			defaultWhere:where,
			width:maxWidth,
			height:maxHeight,
			listeners:{
				accept:function(p,data){
					me.onSaveCheckData(p,data,record);
				}
			}
		}).show();
	}
});