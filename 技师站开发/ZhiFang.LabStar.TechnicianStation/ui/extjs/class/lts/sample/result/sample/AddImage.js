/**
 * 新增图片
 * @author Jcall
 * @version 2020-09-16
 */
Ext.define('Shell.class.lts.sample.result.sample.AddImage',{
	extend:'Shell.ux.form.Panel',
	title:'新增图片',
	width:530,
	height:250,
	bodyPadding:'20px 10px',
	
	//新增Lis_TestGraph
	addLisTestGraphUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestGraph',
	//检验结果图形表数据保存
	addLisTestGraphDataUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestGraphData',

	//布局方式
	layout:{type:'table',columns:4},
	//默认组件类型
	defaultType:'textfield',
	//每个组件的默认属性
	defaults:{
		labelWidth:60,
		labelAlign:'right'
	},
	formtype:'add',
	//是否启用保存按钮
	hasSave:true,
	
	//小组ID
    sectionId:null,
    //检验单数据ID
    testFormId:null,
	//需要生成缩略图的宽高 大于 则等比例去生成
	imgWidth: 200,
	imgHeight: 200,

    //检验图形字段
	//图形编号,图形名称,图形类型,图形图数据ID,图形数据说明,图形备注
	//图形高度,图形宽度,显示次序,是否报告
	FIELDS:[
		'GraphNo','GraphName','GraphType','GraphDataID','GraphInfo','GraphComment',
		'GraphHeight','GraphWidth','DispOrder','IsReport'
	],
	//显示次序默认
	DispOrder:0,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建内部组件
	createItems:function(){
		var me = this;
		
		var items = [
		    {
				colspan: 4, width: 500, fieldLabel: '图片文件', xtype: 'filefield', name: 'File', editable: false, allowBlank: false, itemId: 'imgFile',
				emptyText: '', buttonConfig: { iconCls: 'button-search', text: '选择' }, listeners: {
					change: function (t, value) {
						var str = value.split("fakepath\\")[1];
						t.inputEl.dom.value = str;
						//选择图片时给图形名称赋值
//						var fieldName = str.split('.');
//						me.getComponent('graphName').setValue(fieldName[0]);
					}
				}
			},
			{ colspan: 4, width: 500, fieldLabel: '图形名称', name: 'graphName',itemId:'graphName', allowBlank: false,value:'图形'+me.DispOrder },
			{ colspan: 4, width: 500, fieldLabel: '样本单ID', name: 'testFormID', itemId:'testFormID', hidden: true },
			{ colspan: 1, width: 150, fieldLabel: '图形编号', name: 'graphNo', xtype: 'numberfield',itemId:'graphNo', value: me.DispOrder,allowBlank: false },
			{ colspan: 1, width: 120, fieldLabel: '显示次序', name: 'dispOrder', xtype: 'numberfield', allowBlank: false, value:me.DispOrder,
			listeners:{
				 change : function(com, newValue,oldValue, eOpts ){
				 	me.getComponent('graphNo').setValue(newValue);
				 }
			}},
			{ colspan: 1, width: 150, fieldLabel: '图形类型', name: 'graphType', itemId:'graphType', emptyText: 'JPG,BMP,GIF等'},
			{ colspan: 1, width: 80, boxLabel: '是否报告', name: 'isReport', xtype: 'checkboxfield', style: 'margin-left:15px;', checked: true },
			{ colspan: 4, width: 500, fieldLabel: '数据说明', name: 'graphInfo' },
			{ colspan: 4, width: 500, fieldLabel: '图形备注', name: 'graphComment' },
			
			{ fieldLabel: '图片Base64编码', name: 'graphBase64', hidden: true, itemId: 'graphBase64' },
			{ fieldLabel: '图片缩略图Base64编码', name: 'graphThumb', hidden: true, itemId: 'graphThumb' },
			{ fieldLabel: '图形高度', name: 'graphHeight', itemId:'graphHeight', hidden: true, xtype: 'numberfield' },
			{ fieldLabel: '图形宽度', name: 'graphWidth', itemId: 'graphWidth',hidden:true,xtype:'numberfield'}
		];
		
		return items;
	},
	//更改标题
	changeTitle:function(){
		//不做处理
	},
	//保存
	onSaveClick:function(){
		var me = this;
		me.getComponent("testFormID").setValue(me.testFormId);//赋值testFormID
		if (me.getComponent("graphType").getValue() == "") me.getComponent("graphType").emptyText = "";//表单提交会将emptyText传到后台
		if(!me.getForm().isValid()) return;
		me.addLisGraphData(function () {
			me.fireEvent('save', me);
		});
		//新增Lis_TestGraph
		//me.addLisTestGraph(function(LisTestGraphId){
		//	me.fireEvent('save',me);
		//});
	},
	//新增Lis_TestGraph  -- 不用
	addLisTestGraph:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.addLisTestGraphUrl,
			values = me.getForm().getValues();
			
		var entity = {};
		entity.LisTestForm = {Id:me.testFormId,DataTimeStamp:[0,0,0,0,0,0,0,0]};
		entity.GraphName = values.LisTestGraph_GraphName;
		entity.GraphNo = values.LisTestGraph_GraphNo;
		entity.DispOrder = values.LisTestGraph_DispOrder;
		entity.IsReport = values.LisTestGraph_IsReport ? true : false;
		
		if(values.LisTestGraph_GraphType){
			entity.GraphType = values.LisTestGraph_GraphType;
		}
		if(values.LisTestGraph_GraphType){
			entity.GraphInfo = values.LisTestGraph_GraphInfo;
		}
		if(values.LisTestGraph_GraphType){
			entity.GraphComment = values.LisTestGraph_GraphComment;
		}
		//保存到后台
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode({
			entity:entity
		}),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				callback(data.value.id);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//新增图形数据表
	addLisGraphData: function (callback) {
		var me = this,
			imgFile = me.getComponent("imgFile").getValue(),
			url = JShell.System.Path.ROOT + me.addLisTestGraphDataUrl;
		if (!imgFile) {
			JShell.Msg.alert("请选择图片!");
		}

		//获得上传图片的base64字符串编码
		var file = me.getComponent("imgFile").getEl().dom.querySelector('input[type=file]');
		var re = new FileReader();
		re.readAsDataURL(file.files[0]);
		var base64 = '';
		re.onload = function (re) {
			base64 = re.target.result;//原图片base64编码
			var img = new Image();
			img.src = base64;
			img.onload = function () {
				if (img.naturalHeight > me.imgHeight || img.naturalWidth > me.imgWidth) {
					var width = me.imgWidth, height = me.imgHeight, Scaling = 1;
					if ((img.naturalHeight - me.imgHeight) > (img.naturalWidth - me.imgWidth)) {
						Scaling = img.naturalHeight / me.imgHeight;
					} else {
						Scaling = img.naturalWidth / me.imgWidth;
					}
					width = img.naturalWidth / Scaling;
					height = img.naturalHeight / Scaling;
					me.resizeImage(base64, function (thumbnailBase) {
						me.getComponent("graphHeight").setValue(img.naturalHeight);//赋值图片高
						me.getComponent("graphWidth").setValue(img.naturalWidth);//赋值图片宽
						me.getComponent("graphBase64").setValue(base64);//赋值图片base64编码
						me.getComponent("graphThumb").setValue(thumbnailBase);//赋值图片缩略图base64编码
						me.showMask(me.saveText);//显示遮罩层
						me.submit({
							url: url,
							success: function (form, action) {
								me.hideMask();//隐藏遮罩层
								var data = action.result;
								if (data.success) {
									callback();
								} else {
									JShell.Msg.error("保存失败!");
									return false;
								}
							},
							failure: function (form, action) {
								me.hideMask();//隐藏遮罩层
								var data = action.result;
								JShell.Msg.error('服务错误！' + data.ErrorInfo);
							}
						});	
					}, width, height);
				} else {
					me.getComponent("graphHeight").setValue(img.naturalHeight);//赋值图片高
					me.getComponent("graphWidth").setValue(img.naturalWidth);//赋值图片宽
					me.getComponent("graphBase64").setValue(base64);//赋值图片base64编码
					me.getComponent("graphThumb").setValue(base64);//赋值图片缩略图base64编码
					me.showMask(me.saveText);//显示遮罩层
					me.submit({
						url: url,
						success: function (form, action) {
							me.hideMask();//隐藏遮罩层
							var data = action.result;
							if (data.success) {
								callback();
							} else {
								JShell.Msg.error("保存失败!");
								return false;
							}
						},
						failure: function (form, action) {
							me.hideMask();//隐藏遮罩层
							var data = action.result;
							JShell.Msg.error('服务错误！' + data.ErrorInfo);
						}
					});	
				}
			}
		}
	},
	//根据canvas生成缩略图base64
	resizeImage:function (src, callback, w, h){
		var canvas = document.createElement("canvas"),
		ctx = canvas.getContext("2d"),
		im = new Image();
		w = w || 0,
		h = h || 0;
		im.onload = function () {
			//为传入缩放尺寸用原尺寸
			!w && (w = this.width);
			!h && (h = this.height);
			//以长宽最大值作为最终生成图片的依据
			if (w !== this.width || h !== this.height) {
				var ratio;
				if (w > h) {
					ratio = this.width / w;
					h = this.height / ratio;
				} else if (w === h) {
					if (this.width > this.height) {
						ratio = this.width / w;
						h = this.height / ratio;
					} else {
						ratio = this.height / h;
						w = this.width / ratio;
					}
				} else {
					ratio = this.height / h;
					w = this.width / ratio;
				}
			}
			//以传入的长宽作为最终生成图片的尺寸
			if (w > h) {
				var offset = (w - h) / 2;
				canvas.width = canvas.height = w;
				ctx.drawImage(im, 0, offset, w, h);
			} else if (w < h) {
				var offset = (h - w) / 2;
				canvas.width = canvas.height = h;
				ctx.drawImage(im, offset, 0, w, h);
			} else {
				canvas.width = canvas.height = h;
				ctx.drawImage(im, 0, 0, w, h);
			}
			callback(canvas.toDataURL("image/png"));
		}
	 im.src = src;
	}
});