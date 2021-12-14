/**
 * 移库总单
 * @author liangyl
 * @version 2018-11-05
 */
Ext.define('Shell.class.rea.client.transfer.apply.DocGrid', {
	extend: 'Shell.class.rea.client.transfer.accept.DocGrid',
	
    editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField',
    /**移库管理类型：1-直接移库 ，2-移库管理(申请)，3-移库管理(全部）*/
	TYPE: '2',
	
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  [];
		buttonToolbarItems.push('refresh','-');
		buttonToolbarItems.push({text:'移库申请',tooltip:'移库申请',iconCls:'button-add',
		    itemId:'Add',
			handler: function() {
				me.onAddClick();
			}
		},{
		 text:'继续申请',tooltip:'继续申请',iconCls:'button-edit',itemId:'Edit',
	        handler:function(){
		     	var records = me.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
			   	me.onEditClick(records[0].get('ReaBmsTransferDoc_Id'));
		   }
	    },{
		text:'作废',tooltip:'作废',iconCls:'button-del',itemId:'Invalid',
		   handler:function(){
		   	   var records = me.getSelectionModel().getSelection();
			   if (records.length == 0) {
				   JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				   return;
			   }
		   	   me.onInvalidClick(records[0].get('ReaBmsTransferDoc_Id'));
		   }
		});
		return buttonToolbarItems;
	},
		/**显示表单*/
	showForm: function(IsCheck) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var	config = {
			resizable: false,
			height:height,
			width:maxWidth,
			SUB_WIN_NO: '1',
			formtype:'add',
			IsCheck:'0',
			IsTransferDocIsUse:me.IsTransferDocIsUse,
			TransferScanCode:me.TransferScanCode,
			listeners: {
				save: function(p, records) {
					p.close();
					me.onSearch();
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.transfer.apply.AddPanel', config).show();
	},
	
	/**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	},
	changeBtnDisable:function(){
		var me = this;
	},
	onEditClick : function(id){
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '1',
			PK:id,
			formtype:'edit',
			defaluteOutType: me.defaluteOutType,
			IsTransferDocIsUse:me.IsTransferDocIsUse,
			TransferScanCode:me.TransferScanCode,
			IsCheck:'0',
			IsShowEdit:true,
			listeners: {
				save: function(p, records) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
					p.close();
					me.onSearch();
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.transfer.apply.AddPanel', config).show();
	},
	/**作废*/
	onInvalidClick: function(id) {
		var me = this;
		var msg = '您确定要作废移库单吗';
		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;
			var url = (me.editUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.editUrl;

			var params = {
				Id: id,
				Visible: 0
			};
            var entity = {
            	entity: params,
            	fields:"Id,Visible"
            };
			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
				me.hideMask();
				if(data.success){
				    if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	removeSomeStatusList:function(){
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].List));
		var removeArr = [];
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		
		me.searchStatusValue=tempList;
		return tempList;
	},
	 onBtnChange:function(rec){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
            Add = buttonsToolbar.getComponent('Add'),
            Edit = buttonsToolbar.getComponent('Edit'),
	        Invalid = buttonsToolbar.getComponent('Invalid');
        Add.setDisabled(false);
        Edit.setDisabled(true);
        Invalid.setDisabled(true);
    	if(!rec)return;
        var  Status = rec.get('ReaBmsTransferDoc_Status');  
        switch (Status){
        	case '1':
	        	Edit.setDisabled(false);
	    	    Invalid.setDisabled(false);
        		break;
        	case '2':
        		break;
        	case '3':
	        	Edit.setDisabled(false);
	    	    Invalid.setDisabled(false);
        		break;
        	case '5':
	        	Edit.setDisabled(false);
        		break;
        	default:
        		break;
        }
	},
	//根据类型，赋值
	changeType:function(){
		var me = this;
		me.typeByHQL='1';
	}
});