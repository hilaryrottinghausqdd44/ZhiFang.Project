/**
 * 业务接口配置模块
 * @author liangyl
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.configuration.interface.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '业务接口配置模块',
	
	width:670,
    height:560,
	/**获取数据服务路径*/
//	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo',
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
	/**根据机构获取数据服务路径*/
	selectCenOrgUrl: '/ReaSysManageService.svc/ST_UDTO_SearchCenOrgByHQL?isPlanish=true',
	/**修改数据服务路径*/
	editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBParameterByField',
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**业务接口参数NO*/
	ParaNo:'C-IURL-CONF-0020',
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 120,
		width: 280,
		labelAlign: 'right'
	},
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'edit',
	/**内容周围距离*/
	bodyPadding: '10px 10px 10px 10px',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
	    var arr =[];
	    me.items=me.createItems();
		me.callParent(arguments);
	},
	/**创建组件
	 * arr 组件items
	 * str 显示说明
	 * val 行的object
	 * */
	createCom:function(arr,str,j,val){
		var me =this;
		var url='',AppKey='',AppPassword='';
		var urlval=val.URL;
		var AppKeyval=val.AppKey;
		var AppPasswordval=val.AppPassword;
		url= {fieldLabel:str+'URL',name:j+str+'Url',itemId:j+str+'Url',value:urlval,
			colspan: 2,width: me.defaults.width * 2,
		};
		arr.push(url);
		AppKey= {fieldLabel:str+'AppKey',name:j+str+'AppKey',itemId:j+str+'AppKey',
			value:AppKeyval,colspan: 1,width: me.defaults.width * 1,
		};
		arr.push(AppKey);
		AppPassword= {fieldLabel:str+'AppPassword',name:j+str+'AppPassword',itemId:j+str+'AppPassword',
			value:AppPasswordval,colspan: 1,width: me.defaults.width * 1,
		};
		arr.push(AppPassword);
		return arr ;
	},
	createItems:function(){
		var me = this,
			arr = [];
	    var LabID = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID) || -1;
		var OrgNoVal='',CNameVal='',ShortCodeVal='';
		me.getCenOrg(LabID,function(data){
			if(data && data.value){
				OrgNoVal=data.value.list[0].CenOrg_OrgNo;
				CNameVal=data.value.list[0].CenOrg_CName;
				ShortCodeVal=data.value.list[0].CenOrg_ShortCode;
			}
		});
		me.getParaVal(function(data){
			var id =data.list[0].BParameter_Id;
			var ParaValue = data.list[0].BParameter_ParaValue;
			var strtxts=ParaValue.replace(/<br \/>/g,'')
			var strList = Ext.decode(strtxts);
			var OrgNo=strList.OrgNo;
			var CompCode=strList.CompCode;
			if(!CompCode)CompCode=ShortCodeVal;
			var InfoFieldSet ={
				xtype: 'fieldset',title: '机构信息',
				collapsible: true,defaultType: 'textfield',
				itemId: 'InfoFieldSet',layout:me.layout,
				defaults: me.defaults,colspan: 4,width: me.defaults.width * 4+50,
				items:[{fieldLabel:'主键',emptyText:'',name:'Id',itemId:'Id',hidden:true,
					value:id,locked: true,readOnly: true,colspan: 1,width: me.defaults.width * 1,
				},{fieldLabel:'LabID',emptyText:'LabID',name:'LabID',hidden:true,
					colspan: 1,width: me.defaults.width * 1,value:LabID
				},{fieldLabel:'机构所属平台编码',emptyText:'',name:'OrgNo',locked: true,readOnly: true,
					colspan: 1,width: me.defaults.width * 1,value:OrgNoVal,itemId:'OrgNo',
				},{fieldLabel:'供应商机构码',emptyText:'',name:'CompCode',itemId:'CompCode',
					value:CompCode,colspan: 1,width: me.defaults.width * 1
				},{fieldLabel:'机构名称',emptyText:'',name:'CName',itemId:'CName',
					value:CNameVal,locked: true,readOnly: true,colspan: 1,width: me.defaults.width * 1,
				}]
			}
			arr.push(InfoFieldSet);
			var list=strList.List;
			var len =list.length;
			for(var i =0;i<len;i++){
				var ZFPlatformUrl=list[i].ZFPlatformUrl;
				if(ZFPlatformUrl && ZFPlatformUrl.length>0){
					var ZFPlatSet ={
						xtype: 'fieldset',title: '智方平台Url',collapsible: true,
						defaultType: 'textfield',itemId: 'ZFPFieldSet',
						layout:me.layout,defaults: me.defaults,
						colspan: 4,width: me.defaults.width * 4+50
					}
					for(var j =0; j<ZFPlatformUrl.length;j++){
						var ZFArr=[];
                        if(ZFPlatformUrl[j].ZFPlatformUrl){
                        	var val=ZFPlatformUrl[j].ZFPlatformUrl;
                        	var str ='zf';
							ZFArr = me.createCom(ZFArr,'',str,val);
						}
					}
					ZFPlatSet.items=ZFArr;
					arr.push(ZFPlatSet);
				}
				//订单业务接口
				var OderUrl=list[i].OderUrl;
				if(OderUrl && OderUrl.length>0){
					for(var j =0; j<OderUrl.length;j++){
						
					}
				}
				//供货业务接口
				var SaleUrl=list[i].SaleUrl;
				if(SaleUrl && SaleUrl.length>0){
					var SaleFieldSet ={xtype: 'fieldset',
						title: '供货业务接口',collapsible: true,defaultType: 'textfield',
						itemId: 'SaleFieldSet',layout:me.layout,defaults: me.defaults,
						colspan: 4,width: me.defaults.width * 4+50
					}
					var SaleArr=[];
					for(var j =0; j<SaleUrl.length;j++){
						if(SaleUrl[j].Mike){
							var val=SaleUrl[j].Mike;
							var str ='Sale';
							SaleArr = me.createCom(SaleArr,'Mike',str,val);
						}
						if(SaleUrl[j].Barry){
							var val=SaleUrl[j].Barry;
							var str ='Sale';
							SaleArr = me.createCom(SaleArr,'Barry',str,val);
						}
					}
					SaleFieldSet.items=SaleArr;
					arr.push(SaleFieldSet);
				}
				//验收业务接口
				var ConfirmUrl=list[i].ConfirmUrl;
				if(ConfirmUrl && ConfirmUrl.length>0){
					var ConfirmFieldSet ={xtype: 'fieldset',title: '验收业务接口',
						collapsible: true,defaultType: 'textfield',
						itemId: 'ConfirmFieldSet',layout:me.layout,defaults: me.defaults,
						colspan: 4,width: me.defaults.width * 4+50
					}
		            var ConfirmArr=[];
					for(var j =0; j<ConfirmUrl.length;j++){
						if(ConfirmUrl[j].Mike){
							var val=ConfirmUrl[j].Mike;
							var str ='Confirm';
							ConfirmArr = me.createCom(ConfirmArr,'Mike',str,val);
						}
						if(ConfirmUrl[j].Barry){
							var val=ConfirmUrl[j].Barry;
							var str ='Confirm';
							ConfirmArr = me.createCom(ConfirmArr,'Barry',str,val);
						}
					}
					ConfirmFieldSet.items=ConfirmArr;
					arr.push(ConfirmFieldSet);
				}
			}
		});
		return arr;	
		
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**根据参数加载表单*/
	getParaVal: function(callback) {
		var me = this;	
		var paraVal =0;
		var LabID = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID) || -1;
	    var url = JShell.System.Path.getRootUrl(me.selectUrl);
	    url+="&fields=BParameter_ParaValue,BParameter_Id&where=bparameter.ParaNo='"+me.ParaNo+"' and bparameter.LabID="+LabID;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				if(data && data.value){
					if(callback) callback(data.value);
				}
			} else {
				JShell.Msg.error('获取系统参数(业务接口URL配置为登录人信息)出错！' + data.msg);
			}
		}, false);
	},
	/**根据当前登录者，找到登录者的机构信息*/
	getCenOrg: function(LabID,callback) {
		var me = this;	
		var paraVal =0;
	    var url = JShell.System.Path.getRootUrl(me.selectCenOrgUrl);
	    var fields='&fields=CenOrg_OrgNo,CenOrg_CName,CenOrg_ShortCode';
		var where ='&where=cenorg.LabID='+LabID;
		url+=fields+where;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				if(callback) callback(data);
			} else {
				JShell.Msg.error( data.msg);
			}
		}, false);
	},
	onSaveClick:function(){
		var me =this;
		if(!me.getForm().isValid()) return;
		var values = me.getForm().getValues();
		
		var url =  me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		//机构信息
		var LabID = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID) || -1;
		var InfoFieldSet=me.getComponent('InfoFieldSet');
		var OrgNo=InfoFieldSet.getComponent('OrgNo').getValue();
		var CompCode=InfoFieldSet.getComponent('CompCode').getValue();
		var Id=InfoFieldSet.getComponent('Id').getValue();
		
		//智方平台
		var ZFPFieldSet=me.getComponent('ZFPFieldSet');
		var ZFUrl=ZFPFieldSet.getComponent('zfUrl').getValue();
		var ZFAppKey=ZFPFieldSet.getComponent('zfAppKey').getValue();
		var ZFAppPassword=ZFPFieldSet.getComponent('zfAppPassword').getValue();
		//供货业务接口
		var SaleFieldSet=me.getComponent('SaleFieldSet');
		var SBarryUrl=SaleFieldSet.getComponent('SaleBarryUrl').getValue();
		var SBarryAppKey=SaleFieldSet.getComponent('SaleBarryAppKey').getValue();
		var SBarryAppPassword=SaleFieldSet.getComponent('SaleBarryAppPassword').getValue();
		
		var SMikeUrl=SaleFieldSet.getComponent('SaleMikeUrl').getValue();
		var SMikeAppKey=SaleFieldSet.getComponent('SaleMikeAppKey').getValue();
		var SMikeAppPassword=SaleFieldSet.getComponent('SaleMikeAppPassword').getValue();
		
		//验收业务接口
		var ConfirmFieldSet=me.getComponent('ConfirmFieldSet');
		var BarryUrl=ConfirmFieldSet.getComponent('ConfirmBarryUrl').getValue();
		var BarryAppKey=ConfirmFieldSet.getComponent('ConfirmBarryAppKey').getValue();
		var BarryAppPassword=ConfirmFieldSet.getComponent('ConfirmBarryAppPassword').getValue();
		
		var MikeUrl=ConfirmFieldSet.getComponent('ConfirmMikeUrl').getValue();
		var MikeAppKey=ConfirmFieldSet.getComponent('ConfirmMikeAppKey').getValue();
		var MikeAppPassword=ConfirmFieldSet.getComponent('ConfirmMikeAppPassword').getValue();
		
		var ZFPlatformUrl={ZFPlatformUrl:{URL:ZFUrl,AppKey:ZFAppKey,AppPassword:ZFAppPassword}};
		var OderUrl={};	
        var SaleUrl={
			Mike:{URL:SMikeUrl,AppKey:SMikeAppKey,AppPassword:SMikeAppPassword},
			Barry:{URL:SBarryUrl,AppKey:SBarryAppKey,AppPassword:SBarryAppPassword}};
		var  ConfirmUrl={
			Mike:{URL:MikeUrl,AppKey:MikeAppKey,AppPassword:MikeAppPassword},
			Barry:{URL:BarryUrl,AppKey:BarryAppKey,AppPassword:BarryAppPassword}};
		var ParaValue={LabID:LabID,OrgNo:OrgNo,CompCode:CompCode,List:[{ZFPlatformUrl:[ZFPlatformUrl]},{OderUrl:[OderUrl]},{SaleUrl:[SaleUrl]},{ConfirmUrl:[ConfirmUrl]}]};
        var obj={
        	Id:Id,
        	ParaValue:Ext.JSON.encode(ParaValue)
        };
        var fields='Id,ParaValue';
        var params = Ext.JSON.encode({
			entity:obj,
			fields:fields
        });
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});