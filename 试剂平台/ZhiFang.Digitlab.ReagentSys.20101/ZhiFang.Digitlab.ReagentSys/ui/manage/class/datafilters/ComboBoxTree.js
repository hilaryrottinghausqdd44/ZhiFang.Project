/**
 *  基于Extjs4的下拉树控件
 *  用法:
    
    实例化store作为数据源
    var store = Ext.create('Ext.data.TreeStore',{
		proxy:{
			type:'ajax',
			url:''
		}
	});
	
	实例化ComboBoxTree
	var ctree =Ext.create("Ext.manage.datafilters.ComboBoxTree", {
        id:'c_tree',
        name:'c_tree',
        store :store,
		editable: false,
		multiCascade:false,
		multiSelect : false,
        width : 300
	});
	
	说明：
	1)注意单选时加载的store的对象串中不要有checked字段，多选时加载的store的对象串中一定要有checked字段;
	2)multiSelect属性不写默认是false，是单选;设定为true时是多选;
	3)multiCascade属性不写默认是true,代表多选时是否级联选择;设定为false时不级联选择;
	4)treeHeight属性是指下拉树层的高度，不写默认是300;
	
	常用方法:
	
	清除当前值
	ctree.clearValue();
	
	获得当前选中的提交值
	ctree.getSubmitValue();
	
	获得当前选中的显示值
	ctree.getDisplayValue();
    
    返回选中的所有节点[]
	ctree.getChecked();
    
	单选时获得当前选中的对应树节点的JSON值
	ctree.getEleJson();
	
	设置控件默认值
	单选默认值('树节点id值','树节点text显示值')
	ctree.setDefaultValue('4100000000021119950','北京');
	多选默认值('逗号隔开的树节点id值','逗号隔开的树节点text显示值')
	ctree.setDefaultValue('4100000000021119950,4100000000021119951','北京,上海');
	
	设置控件自动展开定位或选中
	单选展开定位('树上的节点路径')
	ctree.setPathValue('/root/4100000000021119950');
	多选展开选中
	var pathArray = [];
	pathArray[0] = '/root/4100000000021119950';
	pathArray[1] = '/root/4100000000021119951';
	ctree.setPathArray(pathArray);
    
	公开事件
    itemclick:单选选择项选中事件
    onCheckboxChange:多选选择项选中事件
 */
 
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.ComboBoxTree',{
	extend : 'Ext.form.field.Picker',
	requires : ['Ext.tree.Panel'],
	alias : ['widget.comboboxtree'],
	multiSelect : false,
	multiCascade:true,
    nodeClassName:'',
    ParentCName:'',
    ParentEName:'',
    CName:'',
    ClassName:'',
    rootVisible:false,
	submitValue:'',
	pathValue:'',
    columns:null,
	/***
	 * 是否在节点展开后处理其他事情
	 * @type Boolean
	 */
	isbeforeload:true,
	/***
	 * 是否在节点展开后处理其他事情
	 * @type Boolean
	 */
	beforeitemexpand:true,
    fields:['id','parentId','text','expanded','leaf','FieldClass','tid','value','InteractionField','ParentCName','ParentEName'],

    /***
     * 选中项的节点
     * @type 
     */
    selectNodes:[],
	pathArray:[],
    /***
     * 对象字段数组,代替moduleFields
     * 基础字段数组
     * @type 
     */
    defaultFields:[
        {name:'text',type:'auto'},//默认的现实字段
        {name:'expanded',type:'auto'},//是否默认展开
        {name:'leaf',type:'auto'},//是否叶子节点
        {name:'icon',type:'auto'},//图标
        {name:'url',type:'auto'},//地址
        {name:'id',type:'auto'},//id
        {name:'tid',type:'auto'},//默认ID号
        {name:'Id',type:'auto'},//ID
        {name:'ParentID',type:'auto'},//ParentID
        {name:'ParentCName',type:'auto'},//ParentCName
        {name:'ParentEName',type:'auto'},//ParentEName
        {name:'FieldClass',type:'auto'},//字段类型
        {name:'InteractionField',type:'auto'},//字段
        {name:'value',type:'auto'}//
    ], 

	initComponent : function(){
		var self = this;
        self.selectNodes=[];
		Ext.apply(self,{
			fieldLabel : self.fieldLabel,
			labelWidth : self.labelWidth     
		});

        self.addEvents('onCheckboxChange');
        self.addEvents('itemclick');//
		self.callParent();
	},
   getChecked:function(){
        var self=this;
        return self.selectNodes;
    },
    setColumns:function(){
	    var self=this;
        self.column=[{
                xtype:'treecolumn',text:'名称',
                width:200,sortable:true,
                dataIndex:'text'
            },
            {
                text:'InteractionField',
                hidden:true,
                width:10,sortable:true,
                dataIndex:'InteractionField'
            }];
	    return self.column;
    },
	createPicker:function(){
		var self = this;
		self.picker = Ext.create('Ext.tree.Panel',{
			height : self.treeHeight==null?300:self.treeHeight,
            rootVisible:self.rootVisible,
			autoScroll : true,
			floating : true,
            columns:self.columns==null?self.setColumns():self.columns,
			focusOnToFront : false,
			shadow : true,
			ownerCt : this.ownerCt,
			useArrows : true,
			store : self.store,
			viewConfig: {
				onCheckboxChange: function(e,t) {
				if (self.multiSelect) {
					var item = e.getTarget(this.getItemSelector(),this.getTargetEl()), record;
					if (item) {
						record = this.getRecord(item);
						var check = !record.get('checked');
						record.set('checked',check);
						if(self.multiCascade){
							if (check) {
								record.bubble(function(parentNode) {
									if ('Root' != parentNode.get('text')) {
										parentNode.set('checked',true);
									}
								});
								record.cascadeBy(function(node) {
									node.set('checked',true);
									node.expand(true);
								});
							} else {
								record.cascadeBy(function(node) {
									node.set('checked',false);
								});
								record.bubble(function(parentNode) {
									if ('Root' != parentNode.get('text')) {
										var flag = true;
										for (var i = 0; i < parentNode.childNodes.length; i++) {
											var child = parentNode.childNodes[i];
											if(child.get('checked')){
												flag = false;
												continue;
											}
										}
										if(flag){
											parentNode.set('checked',false);
										}
									}
								});
							}
						}
					}
					var records = self.picker.getView().getChecked(), names = [], values = [];
                    self.selectNodes=[];
                    self.selectNodes=records;
                    self.fireEvent('onCheckboxChange',self,records,e,null);
			    	Ext.Array.each(records, function(rec) {
			    		names.push(rec.get('text'));
			    		values.push(rec.get('InteractionField'));
			    	});
			    	self.submitValue = values.join(',');
			    	self.setValue(names.join(','));
				}
				}
			}
		});
		self.picker.on({
			itemclick: function (view,record,item,index,e,object) {
                self.selectNodes=[];
                self.selectNodes.push(record);
                self.fireEvent('itemclick',self,record,e,object);
				if (!self.multiSelect) {
                    var leaf=record.get('leaf');
                    var parentNode=record.parentNode,parentCName="";
                    if(parentNode&&parentNode!=null){
                        parentCName=parentNode.get('text');
                    }
                    if(parentCName=="Root"||parentCName=="数据对象"){
                        parentCName="";
                    }
                    if(leaf&&leaf==true){
                        //如果是子节点时才赋值
                        self.submitValue = record.get('InteractionField');
                        if(parentCName!=''){
	                       self.setValue(parentCName+'.'+record.get('text')); 
                        }else{
                            self.setValue(record.get('text')); 
                        }
	                    self.eleJson = Ext.encode(record.raw);
	                    self.collapse(); 
                    }else{
                        //如果是父节点时不赋值
                        self.submitValue ='';
                        self.setValue(''); 
                        self.eleJson = Ext.encode(record.raw);
                    }
					   
				}
			},
            beforeitemexpand:function(node){ 
                self.fireEvent('beforeitemexpand',node);
                if(self.beforeitemexpand==true){
                    var nodeClassName=node.data[self.objectPropertyValueField];
                    if(nodeClassName!=self.nodeClassName){
                        self.isbeforeload=true;
		                self.ParentCName = node.data['text'];
		                self.ParentEName = node.data[self.objectPropertyValueField];
		                self.nodeClassName = nodeClassName;
                    }else if(nodeClassName==self.nodeClassName){
                        self.isbeforeload=false;
                    }
                }
            },
            beforeload:function(store){
                self.fireEvent('beforeload',store);
                if(self.isbeforeload==true){
		            if(self.nodeClassName != ""){
		                store.proxy.url = self.objectPropertyUrl + "?" + self.objectPropertyParam + "=" + self.nodeClassName;
		            }
                }
	        }
		});
		return self.picker;
	},
	listeners:{
		expand : function(field,eOpts){
			var picker = this.getPicker();
			if(!this.multiSelect){
				if(this.pathValue != ''){
					picker.expandPath(this.pathValue,'id','/',function(bSucess,oLastNode){
						picker.getSelectionModel().select(oLastNode);   
					});
				}
			}else{
				if(this.pathArray.length > 0){
			        for(var m=0;m<this.pathArray.length;m++){
			        	picker.expandPath(this.pathArray[m],'id','/',function(bSucess,oLastNode){
			        		oLastNode.set('checked',true); 
						});
					}
				}
			}
		}
	},
	clearValue : function() {
		this.setDefaultValue('','');
	},
    /**
     * 获取数据对象内容时后台接收的参数名称
     * @type String
     */
    objectPropertyParam:'EntityName',
    /**
     * 获取数据对象内容的服务地址
     * @type 
     */
    objectPropertyUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityFrameTree',
    /**
     * 数据对象内容的值字段
     * @type String
     */
    objectPropertyValueField:'InteractionField',
	getEleJson : function() {
		if(this.eleJson == undefined){
			this.eleJson = [];
		}
		return this.eleJson;
	},
	getSubmitValue : function() {
		if(this.submitValue == undefined){
			this.submitValue = '';
		}
		return this.submitValue;
	},
	getDisplayValue:function(){
		if(this.value == undefined){
			this.value = '';
		}
		return this.value;
	},
	setPathValue:function(pathValue){
		this.pathValue = pathValue;
	},
	setPathArray:function(pathArray){
		this.pathArray = pathArray;
	},
	setDefaultValue:function(submitValue,displayValue){
		this.submitValue = submitValue;
		this.setValue(displayValue);
		this.eleJson = undefined;
		this.pathArray = [];
	},
	alignPicker:function(){
		var me = this, picker, isAbove, aboveSfx = '-above';
		if (this.isExpanded){
			picker = me.getPicker();
			if (me.matchFieldWidth){picker.setWidth(me.bodyEl.getWidth());}
			if (picker.isFloating()){
				picker.alignTo(me.inputEl, "", me.pickerOffset);// ""->tl
				isAbove = picker.el.getY() < me.inputEl.getY();
				me.bodyEl[isAbove ? 'addCls' : 'removeCls'](me.openCls+aboveSfx);
				picker.el[isAbove ? 'addCls' : 'removeCls'](picker.baseCls+aboveSfx);
			}
		}
	}
});