/**
 * 质量数据登记
 * @author liangyl
 * @version 2016-08-26
 */
Ext.define('Shell.class.qms.equip.emaintenancedata.AddPanel', {
	extend: 'Shell.class.qms.equip.register.AddPanel',
	title: '质量数据登记',
	/**是否是新增，最有边列表判断状态*/
	IsAdd:false,
	/**一键保存,0单个保存，1一键保存*/
	IsOnekey:'0',
	createItems: function() {
		var me = this;
		var items = [];
		me.Btn = Ext.create('Shell.class.qms.equip.templet.emaintenancedata.Btn', {
			border: false,
			itemId: 'buttonsToolbar2',
			region: 'north',
			height:26
		});
		me.EditTabPanel = Ext.create('Shell.class.qms.equip.emaintenancedata.EditTabPanel', {
			border: false,
			title: '操作列表',
			region: 'center',
			header: false,
			TempletID: me.TempletID,
			itemId: 'EditTabPanel',
			ISEDITDATE:me.ISEDITDATE,
			TEMPTLETTYPE:me.TEMPTLETTYPE
		});
		me.MemoTabPanel = Ext.create('Shell.class.qms.equip.register.MemoTabPanel', {
			title: '备注和附件',
			region: 'south',
			height: 140,
			header: false,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			TempletID: me.TempletID,
			itemId: 'MemoTabPanel'
		});
		return [me.Btn,me.EditTabPanel, me.MemoTabPanel];
	},
	/**保存
     * isLoadSave 是否初始化保存 ,1初始化保存
     * */
	onSaveInfo:function(curDateStr,entityList,isLoadSave){
		var me = this,
		url = JShell.System.Path.getRootUrl(me.addUrl2);
		var params = {
			entityList: entityList,
			templetID:me.TempletID,
			itemDate:curDateStr,
			typeCode:me.NowTabType
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		   me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url, params, function(data) {
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				if(isLoadSave && isLoadSave=='1'){
					//初始化保存不需要提示
					me.fireEvent('save', me,me.IsTbType);
				}else{
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				    me.fireEvent('save', me,me.IsTbType);
				}
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
	/**根据参数返回是否自动载入保存数据
	 * 隐藏载入功能
	 * */
	onSetPara:function(){
		var me =this;
		var ParaValue='0';
		me.getEParaVal(function(data){
			if(data){
				var len =data.list.length;
				for(var i=0;i<len;i++){
					var ParaNo=data.list[i].EParameter_ParaNo;
					var ParaValue=data.list[i].EParameter_ParaValue;
					switch (ParaNo){
						case 'IsAutoSaveLoadData':
						    //返回是否自动载入保存数据
							if(ParaValue=='1'){
								me.IsAutoSaveLoadData='1';
							}else{
								me.IsAutoSaveLoadData='0';
							}
							break;
						case 'IsSaveAllData':
						    //质量记录登记页面保存按钮保存数据的范围
							if(ParaValue=='1'){
								me.IsSaveAllData='1';
							}else{
								me.IsSaveAllData='0';
							}
						    me.IsOnekey=me.IsSaveAllData;
							break;
						case 'IsSaveDataPreview':
						    //质量记录登记页面保存数据后是否直接预览
							if(ParaValue=='1'){
								me.IsSaveDataPreview='1';
							}else{
								me.IsSaveDataPreview='0';
							}
							break;	
						case 'IsShowLoadDataButton':
						    if(ParaValue=='1'){
								me.IsShowLoadDataButton='1';
							}else{
								me.IsShowLoadDataButton='0';
							}
							me.EditTabPanel.Form.isShowDailyBtn(ParaValue);
							break;		
						default:
							break;
					}
				}
			}
		});
	},
		/**根据日期，类型，模板id查询*/
	getTBWhere2: function(BatchNumber, TempletID,operatedate) {
		var me = this,
			where = '',
			params = [];
		if(TempletID) {
			params.push("emaintenancedata.ETemplet.Id=" + TempletID);
		}
		if(me.NowTabType) {
			params.push("emaintenancedata.TempletTypeCode='" + me.NowTabType + "'");
		}
		
		if(operatedate) {
			params.push("emaintenancedata.ItemDate='" + operatedate + "'");
		}
		if(me.TempletBatNo) {
			params.push("emaintenancedata.TempletBatNo='" + me.TempletBatNo + "'");
		}
		if(BatchNumber) {
			params.push("emaintenancedata.BatchNumber='" + BatchNumber + "' or (emaintenancedata.TempletDataType=1 and emaintenancedata.ETemplet.Id=" + TempletID+
		    " and emaintenancedata.TempletTypeCode='"+ me.NowTabType +"' and emaintenancedata.ItemDate='" + operatedate + "')");
		}
		if(params.length > 0) {
			where = params.join(' and ');
		} else {
			where = '';
		}
		return where;
	},
	/**根据日期，类型，模板id查询*/
	getInternalWhere: function(operatedate, TempletTypeCode, TempletID) {
		var me = this,
			where = '',
			params = [];
		var where = '&templetID='+TempletID + 
		'&itemDate='+operatedate+'&typeCode='+TempletTypeCode;
		if(me.TempletBatNo)where+='&templetBatNo='+me.TempletBatNo;
		return where;
	},
	/**还原TB数据（编辑)*/
	getEditTbData:function(records){
		var me=this,list=[];	
	    var records=me.EditTabPanel.getGridSelect();
		me.TodayDataList=[];
		var operationDate = records.get('操作日期');
		var operatedate = JShell.Date.toString(operationDate,true);
		var startdate = me.getOperateDate();
		//操作时间跟当前时间不一致时，加载的数据为前一天的数据
		if(startdate!=operatedate){
			me.isDaily=true;
		}else{
			me.isDaily=false;
		}
		var BatchNumber=records.get('BatchNumber');
		me.BatchNumber=BatchNumber;
		//还原项目，不包含页备注
		list = me.getMaintenanceData(me.getTBWhere2(BatchNumber, me.TempletID,operatedate));
        if(me.isDaily){
        	me.BatchNumber=null;
		    me.EditTabPanel.Form.setDailyData(list);
		    me.onSetItemMemo(list);
			me.isDaily=false;
        }else{
        	var i = 0,Type = 'C',IsSpreadItemList=null,IsMultiSelect=null;
			Ext.Array.each(me.NowTabData, function(rec) {
				IsSpreadItemList=null;IsMultiSelect=null;
				//判断是已设置效期的组件
		        var InitItemCode = rec['InitItemCode'];
				text = rec['text'];
				if(rec['ItemDataType']) {
					Type = rec['ItemDataType'];
				}
				ItemCode = rec['ItemCode'];
				if(rec['IsSpreadItemList']){
					IsSpreadItemList= rec['IsSpreadItemList'];
				}
				if(rec['IsMultiSelect']){
					IsMultiSelect= rec['IsMultiSelect'];
				}
				if(InitItemCode && Type=='D'){
		             var indexdatestr = InitItemCode.lastIndexOf("\|");  
					InitItemCode  = InitItemCode.substring(indexdatestr + 1, InitItemCode.length);
		        }
				me.EditTabPanel.Form.SetFormData(list, ItemCode, Type, i,IsSpreadItemList,IsMultiSelect,false,InitItemCode);
				i = i + 1;
			});
			me.onSetItemMemo(list);
        }
        me.fireEvent('loadSaveDataClick', list);
	},
	/**一键保存开启时，新增按钮需把各个页签的批号删除
	 *原因：新增时 批号为空
	 * */
	changeDelBatNo:function(){
		var me = this;
		var list = [];
		if(me.IsOnekey=='1' && me.allSaveArr.length>0){
			for(var i=0;i<me.allSaveArr.length;i++){
				var arr=me.allSaveArr[i].objArr;
				var listArr =[];
				for(var j = 0;j<arr.length;j++){					
					if(arr[j].TempletBatNo){
                         delete arr[j].TempletBatNo;
                         delete arr[j].Id;
					}
					listArr.push(arr[j]);
				}
				var obj = {
					comitemId:me.allSaveArr[i].comitemId,
					objArr:listArr
				};						
				list.push(obj);
			}
			me.allSaveArr =list;
		}
	},
	//载入按钮显示隐藏，0隐藏，1显示
	isShowDailyBtn:function(bo){
		var me = this;
		if(!bo)bo='0';
		me.EditTabPanel.Form.isShowDailyBtn(bo);
	},
	changeShowDailyBtn:function(){
		var me = this;
		me.EditTabPanel.Form.isShowDailyBtn(me.IsShowLoadDataButton);
	} 
});