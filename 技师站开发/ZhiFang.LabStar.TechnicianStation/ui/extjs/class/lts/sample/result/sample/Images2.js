/**
 * 结果图片
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.Images2', {
	extend: 'Shell.ux.grid.Panel',

	//获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestGraphByHQL?isPlanish=true',
    //获取LIS图形库图形结果表数据
    getImageInfoUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisGraphData',
    //编辑数据服务路径
    editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestGraphByField',
    /**删除数据服务路径*/
	delUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_DelLisTestGraphData',
    //检验结果图形表数据保存
	addLisTestGraphDataUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestGraphData',
    //小组ID
    sectionId:null,
	//检验单数据
    testFormRecord:null,
	
	//默认加载
	defaultLoad: false,
	//不加载时默认禁用功能 按钮
	defaultDisableControl: false,
	//是否启用序号列
	hasRownumberer: false,
	//带功能按钮栏
	hasButtontoolbar: true,
	//带分页栏
	hasPagingtoolbar: false,
	//后台排序
	remoteSort: false,
	//是否开启排序
	sortableColumns: false,
	//排序字段
	defaultOrderBy: [
		{ property: 'LisTestGraph_DispOrder', direction: 'ASC' }
	],
	//隐藏列title
    hideHeaders:true,
    /**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,
	layout:'fit',
	
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.on({
            itemdblclick : function(com, record,item,index,e,eOpts){  //双击大图片
            	var maxWidth = document.body.clientWidth - 20;
		        var maxHeight = document.body.clientHeight - 20;
            	var GraphHeight = Number(record.data.LisTestGraph_GraphHeight)+29;
            	var GraphWidth = Number(record.data.LisTestGraph_GraphWidth)+5;
            	if(GraphHeight>maxHeight)GraphHeight = maxHeight;
            	if(GraphWidth>maxWidth)GraphWidth = maxWidth;
            	if(record.get('LisTestGraph_GraphDataID')){
					JShell.Win.open('Ext.panel.Panel',{
						title:'大图片',width:GraphWidth,height: GraphHeight,autoScroll:true,
						GraphDataID:record.get('LisTestGraph_GraphDataID'),
						listeners:{
							afterrender:function(p){
								me.getImageInfo(1,record.get('LisTestGraph_GraphDataID'),function(data){
									if (data.success) {
										p.add({
											xtype: 'image', width: record.get('LisTestGraph_GraphWidth'), height: record.get('LisTestGraph_GraphHeight'),
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
           },
           itemcontextmenu:function(view,record,item,index,e,eOpts){ //右键菜单
	    		//禁用浏览器的右键相应事件
	    		e.preventDefault();
                e.stopEvent();
                //检验中才允许操作
                if(me.testFormRecord.get('LisTestForm_MainStatusID')=='0')me.createContextMenu(record,item,index,e);
	        },
	        nodata:function(){
	        	me.showErrorInView('没有找到图片');
	        },
	        afterdrop:function(c,node, data, dropRec, dropPosition){//拖拽
	        	//重新排序
                me.reorder();
                //次序改变后保存
                me.onSaveClick();
	        }
        });
	},
	initComponent: function () {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		
		me.initEvents('afterdrop');
		
		me.viewConfig = me.viewConfig || {};
		me.viewConfig.plugins = {
			ptype: 'gridviewdragdrop',dragText:'{0}张图片正在拖动',
			dragGroup: 'firstGridDDGroup',dropGroup: 'firstGridDDGroup'
		};
		me.viewConfig.listeners = {
			drop: function(node, data, dropRec, dropPosition) {
				me.fireEvent('afterdrop',me,node, data, dropRec, dropPosition);
			}
		};

		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'主键ID',dataIndex:'LisTestGraph_Id',width:190,isKey:true,hidden:true,hideable:false
		},{
			text:'GraphDataID',dataIndex:'LisTestGraph_GraphDataID',width:190,hidden:true
		},{
			text:'GraphNo',dataIndex:'LisTestGraph_GraphNo',width:120,hidden:true,defaultRenderer:true
		},{
			text:'GraphName',dataIndex:'LisTestGraph_GraphName',width:120,hidden:true,defaultRenderer:true
		},{
			text:'GraphType',dataIndex:'LisTestGraph_GraphType',width:120,hidden:true,defaultRenderer:true
		},{
			text:'GraphInfo',dataIndex:'LisTestGraph_GraphInfo',width:120,hidden:true,defaultRenderer:true
		},{
			text:'GraphHeight',dataIndex:'LisTestGraph_GraphHeight',width:120,hidden:true,defaultRenderer:true
		},{
			text:'GraphWidth',dataIndex:'LisTestGraph_GraphWidth',width:120,hidden:true,defaultRenderer:true
		},{
			text:'DispOrder',dataIndex:'LisTestGraph_DispOrder',width:60,hidden:true,defaultRenderer:true
		},{
			text:'IsReport',dataIndex:'LisTestGraph_IsReport',width:120,hidden:true,defaultRenderer:true
		},{
			text:'',dataIndex:'ImageSrc',flex:1,
			renderer:function(value,meta,record){
				var height=Number(record.get('LisTestGraph_GraphHeight'))>100 ? 100 : Number(record.get('LisTestGraph_GraphHeight'));
				var GraphWidth=record.get('LisTestGraph_GraphWidth')>130 ? 130 : Number(record.get('LisTestGraph_GraphWidth'));
//				meta.attr = 'style="margin:0px 15px 0px 0px;padding:0;height:100px;width:130px;"';
				return "<img src='"+value+"' height="+height+" width="+GraphWidth+" />";
			}
		}];
		
		return columns;
	},
		/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		
		for(var i =0;i<data.list.length;i++){
			this.getImageInfo(0,data.list[i].LisTestGraph_GraphDataID,function(data2){
				if (data2.success) {
					data.list[i].ImageSrc = data2.value;
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		}		
		return data;
	},
		//获取图形信息
	getImageInfo:function(graphSizeType,graphDataID,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getImageInfoUrl;
			
		url += '?graphSizeType=' + graphSizeType + '&graphDataID=' + (graphDataID ? graphDataID : 0);
		
		JShell.Server.get(url,function(data){
			callback(data);
		},false);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this;

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: me.sectionId ? [{
		    	iconCls:'button-add',text:'新增',tooltip:'新增',itemId:'add',
		    	handler:function(but){me.onAddClick()}
		    },{
		    	iconCls:'button-add',text:'粘贴',tooltip:'粘贴',itemId:'paste',
		    	handler:function(but){
		    		//获取剪贴板的图片
		    		async function getClipboardContents() {
					    try {
						    const clipboardItems = await navigator.clipboard.read();
						    for (const clipboardItem of clipboardItems) {
						        for (const type of clipboardItem.types) {
						            const blob = await clipboardItem.getType(type);
						            if(!blob){
										JShell.Msg.alert("请截图!");
										return;
								    }
						            var typeStr = blob.type.replace(/(^\s*)|(\s*$)/g, "");
						            if(typeStr!='image/png'){
										JShell.Msg.alert("请截图!");
										return;
								    }
						            let files = new File([blob], '图形'+me.store.data.items.length+1, {type: blob.type});
						            me.addLisGraphData(files);
						        }
						    }
						} catch (err) {
							JShell.Msg.alert("请重新截图!");
//						    console.error(err.name, err.message);
						}
					}
		    		getClipboardContents();
		    	}
		    }] : []
		});
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		//新增按钮可见
        if(me.testFormRecord )me.isShowBtn(me.testFormRecord.get('LisTestForm_MainStatusID')=='0' ? true : false);
		me.enableControl(); //启用所有的操作功能
		
      	if (me.errorInfo) {
			me.showErrorInView(me.errorInfo);
			me.errorInfo = null;
		} else {
			if (!records || records.length <= 0) {
				me.showErrorInView(JShell.Server.NO_DATA);
			}
		}

		if (!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
		//默认选中处理
		me.doAutoSelect(records, me.autoSelect);
	},
	/**查询数据*/
	onSearch: function(testFormRecord) {
		var me = this;
		if(testFormRecord)me.testFormRecord = testFormRecord;
		
		me.defaultWhere = "listestgraph.LisTestForm.Id="+me.testFormRecord.get('LisTestForm_Id');
		me.load(null, true, false);
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
			DispOrder:me.nextNum(),
			listeners:{
				save:function(p){
					p.close();
					me.onRefreshClick();
				}
			}
		}).show();
	},
	onRefreshClick:function(){
		var me = this;
		me.onSearch();
	},
	/**初始化右键菜单*/
	initContextMenu: function() {
		var me = this;
		me.on(
			"itemcontextmenu",
			function(view, record, item, rowIndex, e, eOpts) {
				//禁用浏览器的右键相应事件
	    		e.preventDefault();
                e.stopEvent();
				me.onRowContextMenu(e);
			}
		);
	},
	/**创建右键菜单项*/
	createContextMenu: function(record,item,index,e) {
		var me = this;
		var menu = new Ext.menu.Menu({
	    	//控制右键菜单位置
		   	float:true,
		    items:[{xtype: 'label',text: record.data.LisTestGraph_GraphName,margin: '5 0 5 5',style: "font-weight:bold;color:blue;"},{text:'删除',iconCls:'delete',handler:function(){
		      		me.onDelClick();
			     }
		    },{
	     	    text:"上移",iconCls:'button-up',handler:function(){
		            if (index > 0 ) { 
		                me.store.remove(record);  
		                me.store.insert( index -1, record );        //如果不是第一条的话，则将此数据插入到上一条的上一行
		                 //重新排序
		                me.reorder();
	                    //次序改变后保存
		                me.onSaveClick();
		            }  
		      	}
	     	},{
	     		text:"下移",iconCls:'button-down',handler:function(){
		            if (index < me.store.data.items.length - 1 ) { 
		               me.store.remove(record);  
		               me.store.insert( index +1 , record ); //不是最后一条的话，则将此数据插入到此行的下一行的下一行 
		               //重新排序
		               me.reorder();
		               //次序改变后保存
		               me.onSaveClick();
		            }  
		      	}
	     	},{
	     		text:"名称修改",iconCls:'button-edit',handler:function(){
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">名称修改</div>',
						msg: '',
						closable: false,
						multiline: true //多行输入框
					}, function(but, text) {
						if(but != "ok") return;
						me.onEditName(record.data.LisTestGraph_Id,text);
					});
		      	}
	     	}]
	   }).showAt(e.getXY());//让右键菜单跟随鼠标位置
	},
	/**
	 * 重新排序DispOrder
	 * @private
	 */
	reorder:function(){
		var me = this,
		    records=  me.store.data.items,
			length =  records.length,
			list = [],
			index1,index2,temp,num;
		for(var m=0;m<length;m++){	
			list.push(m);
		}
		for(var i=0;i<length-1;i++){
			for(var j=i+1;j<length;j++){
				index1 = parseInt(records[j].LisTestGraph_DispOrder);
				index2 = parseInt(records[i].LisTestGraph_DispOrder);
				if(index1 < index2){
					temp = list[i];
					list[i] = list[j];
					list[j] = temp;
				}
			}
		}
		
		for(var i=0;i<length;i++){
			num = (i + 1) + '';
			records[list[i]].set('LisTestGraph_DispOrder',num);
		}
	},
	//次序调整保存
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var DispOrder = rec.get('LisTestGraph_DispOrder');
			me.updateOne(i,id,DispOrder);
		}
	},
	//更新序号与编号,graphNo图形编号跟次序是一样的
	updateOne:function(index,id,DispOrder){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {};
		params.entity = {Id:id,DispOrder:DispOrder,GraphNo:DispOrder};
		params.fields = 'Id,DispOrder,GraphNo';
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
//					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	//名称修改
	onEditName:function(id,GraphName){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {};
		params.entity = {Id:id,GraphName:GraphName};
		params.fields = 'Id,GraphName';
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//操作按钮可见
    isShowBtn : function(bo){
    	var me = this;
    	me.getComponent("buttonsToolbar").setVisible(bo);
    },
    //新增图形数据表
	addLisGraphData: function (imgFile) {
		var me = this,
			num = me.nextNum(),	
			url = JShell.System.Path.ROOT + me.addLisTestGraphDataUrl;
			
		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file',itemId:'file',value:imgFile},
				{xtype:'textfield',fieldLabel: '图形名称', name: 'graphName',itemId:'graphName', value:'图形'+num },
				{ xtype:'textfield',fieldLabel: '样本单ID', name: 'testFormID', itemId:'testFormID',value:me.testFormRecord.get('LisTestForm_Id')},
				{xtype:'textfield', fieldLabel: '图形编号', name: 'graphNo', xtype: 'numberfield',itemId:'graphNo', value: num },
				{xtype:'textfield', fieldLabel: '显示次序', name: 'dispOrder',itemId:'dispOrder', xtype: 'numberfield', value:num},
				{xtype:'textfield', fieldLabel: '图形类型', name: 'graphType', itemId:'graphType', emptyText: ''},
				{ xtype:'textfield', fieldLabel: '数据说明', name: 'graphInfo' },
				{ xtype:'textfield', fieldLabel: '图形备注', name: 'graphComment' },
				{ xtype:'textfield',fieldLabel: '图片Base64编码', name: 'graphBase64', hidden: true, itemId: 'graphBase64' },
				{ xtype:'textfield',fieldLabel: '图片缩略图Base64编码', name: 'graphThumb', hidden: true, itemId: 'graphThumb' },
				{fieldLabel: '图形高度', name: 'graphHeight', itemId:'graphHeight', hidden: true, xtype: 'numberfield' },
				{ fieldLabel: '图形宽度', name: 'graphWidth', itemId: 'graphWidth',hidden:true,xtype:'numberfield'}
			]
		});
		var re = new FileReader();
        re.readAsDataURL(imgFile);       
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
	                   me.ajaxsubmit(base64,img,num);
					}, width, height);
				} else {
					me.ajaxsubmit(base64,img,num);
				}
			}
		}
	},
	ajaxsubmit : function(base64,img,num){
		var me = this,
		    url = JShell.System.Path.ROOT + me.addLisTestGraphDataUrl;
		me.UpdateForm.getComponent("file").setValue(base64);//赋值图片高
		me.UpdateForm.getComponent("graphHeight").setValue(img.naturalHeight);//赋值图片高
		me.UpdateForm.getComponent("graphWidth").setValue(img.naturalWidth);//赋值图片宽
		me.UpdateForm.getComponent("graphBase64").setValue(base64);//赋值图片base64编码
		me.UpdateForm.getComponent("graphThumb").setValue(base64);//赋值图片缩略图base64编码				
	    
	    //名称：图形+1 ,显示次序于graphNo 一样
	    me.UpdateForm.getComponent("dispOrder").setValue(num);
		me.UpdateForm.getComponent("graphNo").setValue(num);//赋值图片base64编码
		me.UpdateForm.getComponent("graphName").setValue('图形'+num);//赋值图片缩略图base64编码				
		
		me.showMask(me.saveText);//显示遮罩层
		me.UpdateForm.submit({
			url: url,
			success: function (form, action) {
				me.hideMask();//隐藏遮罩层
				var data = action.result;
				if (data.success) {
					me.onSearch()
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
	},
	//下一显示次序
	nextNum : function(){
		var me = this,
		    num = 1 ,
		    items = me.store.data.items;
		if(items.length==0)return num;
		//取最后一行数据
		var DispOrder = me.store.data.items[items.length-1].data.LisTestGraph_DispOrder;
		if(DispOrder)num = Number(DispOrder)+1;
		return num;
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.store.removeAll(); //清空数据
	}
});