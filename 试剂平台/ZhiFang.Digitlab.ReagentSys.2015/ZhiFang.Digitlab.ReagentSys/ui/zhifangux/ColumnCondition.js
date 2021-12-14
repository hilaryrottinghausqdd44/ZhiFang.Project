/**
 * 根据数据对象，展现数据列进行权限设置
 * 列权限可将指定的列设置为不可见、只读、可见并可编辑三种。
 *不可见：对该列没有任何权限
 *只读：对该列只有只读权限
 *可见并可编辑：对该列有完全权限。

 * @param
 * DataServerUrl：服务地址
 * SaveServerUrl：保存地址
 * KeyField：主键值
 * Field：字段值
 * ValueField：值字段
 * Root：读取数据时的根节点
 * okText：按钮text（保存）
 * cancelText：按钮text（取消）
 * WinWidth:窗体宽
 * WinHight：窗体高
 * depDataServerUrl：部门服务地址
 * Name：部门查询文本框名称项
 * ID：部门文本框ID项
 * DepWidth：部门选择器宽
 * DepHeight：部门选择器宽
 * UsersValueField：人员选择器--人员部门数据列表值字段,需要外部传入数据适配
 * UsersDeptValueField：部门下拉绑定树选择器--数据列表值字段,需要外部传入数据适配
 * UsersDataServerUrl：部门人员选择器--人员后台查询服务地址
 * UsersDeptDataServerUrl:部门下拉绑定树选择器--部门后台服务查询地址
 * UsersDeptId：人员部门Id
 * UsersDeptpName：人员部门名称
 * UsersId：匹配部门人员选择器的返回结果里的人员Id值字段
 * UsersName：匹配部门人员选择器的返回结果里的人员名称值字段
 * UsersWidth：人员选择器宽
 * UsersHeight：人员选择器高
 * postDataServerUrl：岗位服务地址
 * postWidth：岗位选择器宽
 * postHeight：岗位选择器高
 * PositionDataServerUrl：职位服务地址
 * PositionWidth：职位选择器宽
 * PositionHeight：职位选择器高
 * 对外公开事件
 * OnSave 保存
 * OnCancel 撤销触发
 */

//列数据条件
Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');
Ext.Loader.setPath('Ext.ux', getRootPath()+'/ui/extjs/ux');
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.ColumnCondition', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.columncondition',
    requires: [
       'Ext.zhifangux.PeoplePicker', 'Ext.zhifangux.DepSelector',
       'Ext.ux.CheckColumn','Ext.zhifangux.UXRadioGroup'
    ],
    layout: 'border',
    //border: false,
    frame: false,
    padding: 5,
	t:null,
    CategoryValue: '', //角色类别临时变量
    FieldValue: '', //字段临时变量（名称）
    FieldRelationValue: '', //字段关系临时变量（id）
    id:'',
   //va:[],
	//公共属性：
    WinWidth:480,
    WinHight:300,
    DataServerUrl:'',//服务地址
    SaveServerUrl:'',//保存地址
    KeyField:'id',
    Field:[],
    Root:'ResultDataValue',//读取数据时的根节点
    ValueField:[],//数据列表值字段,可以是外面做好数据适配后传进来
    okText: '保存',
    cancelText: '撤销当前权限配置',
    formvalue:'',
    //部门
    depDataServerUrl:'',//服务地址
    Name:'',//查询文本框名称项
    ID:'',//查询文本框ID项
    DepWidth:460,//部门选择器宽
    DepHeight:260,//部门选择器高
    DefaultRootProperty:'ResultDataValue',
    
    //一.部门人员选择器
    UsersValueField:[],//部门人员选择器--人员部门数据列表值字段,需要外部传入数据适配
    UsersDeptValueField:[],//部门下拉绑定树选择器--数据列表值字段,需要外部传入数据适配
    UsersDataServerUrl: '', //部门人员选择器--人员后台查询服务地址
    UsersDeptDataServerUrl:'', //部门下拉绑定树选择器--部门后台服务查询地址
    UsersDeptId:'',      //人员部门Id
    UsersDeptpName:'', //人员部门名称 
    UsersId:'',      //匹配部门人员选择器的返回结果里的人员Id值字段
    UsersName:'',   //匹配部门人员选择器的返回结果里的人员名称值字段
    UsersWidth:460,//人员选择器宽
    UsersHeight:255,//人员选择器高
    
    //岗位
    postDataServerUrl:'',//岗位服务地址
   // postradiogroupName:'',//岗位
    postWidth:460,//岗位选择器宽
    postHeight:230,//岗位选择器高
    
    //职位
    PositionDataServerUrl:'',//职位服务地址
   // PositionradiogroupName:'',
    PositionWidth:460,//职位选择器宽
    PositionHeight:230,//职位选择器高
    
	//公共方法:
    
	
	//公共事件:
   // onChangedPost
    /**
     * 解析处理传列表值字段,
     * 封装store数据的Field数组
     * 
     */
     SetField: function () {
         var me = this;
         if (me.ValueField.length > 0) {
             for (var i = 0; i < me.ValueField.length; i++) {
                 var test = me.ValueField[i].dataIndex;
                 me.Field.push(test);
             }
         }
     },
 	
   //获取数据
     gridstore: function () {
  	   // var me = this;
  	    var grid = Ext.create('Ext.data.Store', {
  	        idProperty:this.KeyField,
  	    	fields:this.Field,
  	       // method: 'POST',
  	        timeout: 2000,
  	        proxy: {
  	            type: 'ajax',
  	           // url: this.DataServerUrl+"?id="+id,
  	            url: this.DataServerUrl,
  	            getMethod: function(){ return 'GET'; },//亮点，设置请求方式,默认为GET 
  	            reader: {
  	                type: 'json',
  	                root:this.Root
  	            }
  	       }
  	    });
  	    
  	    grid.load();
  	    return grid;
  	},

    //一个窗口装载4个类型
    OpenWin:function(role){
        var me = this;
        var xtype2=null;
        var xy = me.getPosition();
        switch(role){
        //部门
            case 'dep':
            if(!xtype2){
            	xtype2 = Ext.create('widget.depselector', {
            	height:me.DepHeight,        //容器高度像素
  				width:me.DepWidth,       //容器宽度像素
  				DefaultRootProperty:me.DefaultRootProperty,
        	    DataServerUrl:me.depDataServerUrl,SelectType:0,itemId:'dep',  
                listeners: {
                    onCancelCilck: function () {
                        var bm = Ext.getCmp('bm');
                        bm.close();
                    },
                    OnSave: function () {
                         var arr=this.getAllValue();//Ext.encode(this.lastValues);
                         Ext.each(arr,function(item,index,allItems){
                             Ext.getCmp("field_name").setValue(item[me.Name]);
                             Ext.getCmp("field_id").setValue(item[me.ID]);
                         });
                         
                         var t =Ext.getCmp('grid').getStore();
                         t.load();
                         
                         var bm = Ext.getCmp('bm');
                         bm.close();
                    },
                    OndbClick: function () {
                        var arr=this.getAllValue();//Ext.encode(this.lastValues);
                        Ext.each(arr,function(item,index,allItems){
                            Ext.getCmp("field_name").setValue(item[me.Name]);
                            Ext.getCmp("field_id").setValue(item[me.ID]);
                        });
                        
                        var t =Ext.getCmp('grid').getStore();
                        t.load();
                        
                        var bm = Ext.getCmp('bm');
                        bm.close();
                   }
                    
                    
                }	
              });
           }
           break;
           //人员
           case 'emp':
    	   if (!xtype2) {
    		   xtype2 = Ext.create('widget.peoplepicker', {
			   title: '部门人员选择器',
			   //itemId:'PeoplePicker_temp',
			   titleAlign :"center",
			   autoScroll : true,
			   height:me.UsersHeight,        //容器高度像素
			   width:me.UsersWidth,       //容器宽度像素
			   multiSelect:false,//单选或多选开关; false:单选,true:多选
			   simpleSelect:false,//单选或多选开关;false:单选,true:多选,必须要设置该值,单选或多选才生效
			   //margin :  '11 10 10 332',
			   DataServerUrl:me.UsersDataServerUrl, //人员后台查询服务地址
			   DeptDataServerUrl:me.UsersDeptDataServerUrl, //部门后台服务地址
			   DeptId:me.UsersDeptId,      //人员部门Id
			   DeptpName:me.UsersDeptName,   //人员部门名称    
			   ValueField:me.UsersValueField,//人员部门数据列表值字段,需要外部数据适配后传进来
			   DeptValueField:me.UsersDeptValueField,//部门数据列表值字段,需要外部做好数据适配后传进来
			   UserRoot:me.UserRoot,//部门人员获取/读取后台数据时的根节点
			   DeptRoot:me.DeptRoot,//部门获取/读取后台数据时的根节点
               listeners: {
                   onCancelCilck: function () {
                       // me.t.hide();
	    			   var bm = Ext.getCmp('bm');
	                   bm.close();
                   },
                   onOKCilck: function () {
                	   var arr=this.getAllValue();//Ext.encode(this.lastValues);
                       Ext.each(arr,function(item,index,allItems){
                           Ext.getCmp("field_name").setValue(item[me.UsersName]);
                           Ext.getCmp("field_id").setValue(item[me.UsersId]);
                       });

                       var t =Ext.getCmp('grid').getStore();
                       t.load();
                       
                       var bm = Ext.getCmp('bm');
                       bm.close();
                   }
                 }
             });
           }
           break;
	        //职位   
	       case 'position':
	       if(!xtype2){
            	xtype2 = Ext.create('Ext.panel.Panel', {
            	    border:false,
            	    items: [{
                        xtype: 'uxradiogroup',
                        region: 'north',
                        serverUrl:me.PositionDataServerUrl,
                        radiogroupName:'rb',
                        Width:me.PositionWidth,
                        Height:me.PositionHeight,
                        labField:'',
                        id:'position',
                        butOkBool:true,     //是否隐藏确定按钮
                        butCancleBoll:true, //是否隐藏取消按钮
                        listeners: {
             	    	   beforerender:function(){
             	    	        var post =Ext.getCmp('position');
             	    	        post.setValue(1);
             	           }
             	       }
                      
                    },
                    {xtype: 'panel',region: 'center', height: 34,title: '',
                    layout:'absolute',
                    border:false,
                    items:[
                        {xtype: 'button',text: '确定',x: 190,y: 5,
                       	handler: function () {
                       	   var post=Ext.getCmp('position');
                       	   var arr=post.GetLastValue();//Ext.encode(this.lastValues);
                           Ext.each(arr,function(item,index,allItems){
                               Ext.getCmp("field_name").setValue(item['boxLabel']);
                               Ext.getCmp("field_id").setValue(item['inputValue']);
                           });

                           var t =Ext.getCmp('grid').getStore();
                           t.load();
                           
                           var bm = Ext.getCmp('bm');
                           bm.close();  
                         }   
                          },
                          {xtype: 'button',text: '取消',x: 240,y: 5,
                           handler: function () {
                         	   var bm = Ext.getCmp('bm');
                               bm.close();
                              }   
                          }
                      ]
                }]
             });
           }
	       break;   
                
            //岗位   
           case 'post':
    	   if(!xtype2){
            	xtype2 = Ext.create('Ext.panel.Panel', {
            	    border:false,
            	    Width:460,
                    Height:420,
            	    items: [{
                       xtype: 'uxradiogroup',
                       region: 'north',
                       serverUrl:me.postDataServerUrl,
                       radiogroupName:'rb',
                       Width:me.postWidth,
                       
                       labField:'',
                       Height:me.postHeight,
                       id:'post',
                       butOkBool:true,     //是否隐藏确定按钮
                       butCancleBoll:true, //是否隐藏取消按钮
                       listeners: {
            	    	   beforerender:function(){
            	    	        var post =Ext.getCmp('post');
            	    	        post.setValue(1);
            	           }
            	       }
                       
                   },{
                       xtype: 'panel',
                       region: 'center',
                       height: 34,
                       title: '',
                       layout:'absolute',
                       border:false,
                       items:[
                           {xtype: 'button',text: '确定',x: 190,y: 5,
                        	handler: function () {
                        	   var post=Ext.getCmp('post');
                        	   var arr=post.GetLastValue();//Ext.encode(this.lastValues);
                               Ext.each(arr,function(item,index,allItems){
                                   Ext.getCmp("field_name").setValue(item['boxLabel']);
                                   Ext.getCmp("field_id").setValue(item['inputValue']);
                                 
                               });
                               
                               var t =Ext.getCmp('grid').getStore();
                               t.load();
                               
                               var bm = Ext.getCmp('bm');
                               bm.close();
                               
                             //  me.fireEvent('OnSavepost');
                               } 
                           
                           },
                           { xtype: 'button',text: '取消',x: 240,y: 5,
                            handler: function () {
                          	    var bm = Ext.getCmp('bm');
                                bm.close();
                                
                                //me.fireEvent('OnClearpost');
                               }   
                           }
                       ]
                   }
               ]
            	  
            });
          }
           break;  
            
        default:'';	
	    }
    	if (!me.t) {
            t = new Ext.window.Window({
                width: me.WinWidth,
                height:me.WinHight,
                maxHeight:me.WinHight,
                maxWidth:me.WinWidth,
                x: xy[0] + 38, y: xy[1] + 10,
                layout: {
                    type: 'border',
                    padding: 5
                },
                id: 'bm',
                border: false,
                bodyBorder: false,
                items: [
                    {xtype:xtype2}
                ]
              });
         	 t.show();
          }
          else {t.show(); }
    },
    
    /**
    * 常量设置
    * @private
    */
    initConstants: function () {
        var me = this;
		if (me.Field.length == 0) {
	         me.SetField();
	    }
        //赋初始值
        if (me.CategoryValue === undefined && me.CategoryValue == null) {
            
            me.CategoryValue = Ext.getCmp("FormParamsView_BM").getValue(); //角色类别临时变量
        }
        else if (me.FieldValue === undefined && me.FieldValue == null) {
            me.FieldValue = Ext.getCmp("field_name").getValue(); //字段临时变量（名称）
        }
        else if (me.FieldRelationValue === undefined && me.FieldRelationValue == null) {
            me.FieldRelationValue = Ext.getCmp("field_id").getValue(); //字段关系临时变量（id）
        }
    },
    /**
    * 生成查询条件
    * @private
    * @return {}
    */
    createForm: function () {
        var me = this;
        var form = {
            xtype: 'form', region: 'north', height: 35, layout: 'absolute', bodyPadding: 10, border: false,
            id:'form1',
            fieldDefaults: {//统一设置表单字段默认属性
                labelWidth: 60, //标签宽度
                width: 130,
                labelAlign: 'right'
            },
            items: [
                { xtype: 'combobox', x: 0, y: 5, fieldLabel: '按', width: 150, labelWidth: 30, name: 'text',id:'combo',
                id: 'FormParamsView_BM',mode : 'local',editable : false, displayField: 'text',valueField: 'value',value:'dep',
                store: new Ext.data.SimpleStore({ 
          			fields : ['value','text'], 
          			data : [['dep','部门'],['post','岗位'],['position','职位'],['emp','人员']]
          		}),
          		listeners: {
                    change: function (newValue, oldValue, eOpts) {
                        me.CategoryValue = newValue.value;
                        
                        var field_name=Ext.getCmp('field_name');
                	    var field_id=Ext.getCmp('field_id');
                	    field_name.setValue();
                	    field_id.setValue();
                	 
                    }
                }
                },
                {
                    xtype: 'button', x: 170, y: 5,
                    text : '按名称选择',
                    listeners:{
                	    click:function(){
                	         var role = Ext.getCmp("FormParamsView_BM").getValue();
                             //  创建和弹出选择器
                	         me.OpenWin(role);
                        }
                    }
                },

                { xtype: 'textfield', x: 250, y: 5,id:'field_name', width: 100,
                 labelWidth: 30, name: 'text',value:'', readOnly:true,
                 listeners:{
//                	focus:function(){
//                	   var role = Ext.getCmp("FormParamsView_BM").getValue();
//
//                       //创建和弹出选择器
//                       me.OpenWin(role);
//                    } 
                }  
               }, 
               { xtype: 'textfield', x: 350, y: 5, fieldLabel: 'ID', width: 140, 
            	   labelWidth: 30, name: 'leaf',id:'field_id',value:'',
            	   readOnly:true },
               { xtype: 'button', x: 520, y: 5,text :me.okText,
                handler: function () {
            	   
            	    var com1=Ext.getCmp('FormParamsView_BM').getValue();
            	    var field_name=Ext.getCmp('field_name').getValue();
            	    var field_id=Ext.getCmp('field_id').getValue();
            	    
            	    if (field_id=='' || field_name=="")
	            	   {
	            	      Ext.MessageBox.show({
	     		               title: "提示",
	     		               msg: "请选择部门或岗位或人员或职位详细信息！"
	     		          });
	            		  return;
	            	   }
            	 
            	    var dd={"value":com1,"inputValue":field_name,"boxLabel":field_id};
            	  
                    
    		        var store=Ext.getCmp('grid').getStore();
    		        var records = store.getUpdatedRecords(); // 获取修改的行的数据，无法获取幻影数据
    		        var phantoms = store.getNewRecords(); //获得幻影行
    		        records = records.concat(phantoms); //将幻影数据与真实数据合并
    		        
    		    
    		        if (records.length == 0) {
    		            Ext.MessageBox.show({
    		                title: "提示",
    		                msg: "没有任何数据被修改过!"
    		            });
    		            return;
    		        } else {
    		        	 
    		            Ext.Msg.confirm("请确认", "是否真的要修改数据？", function (button, text) {
    		                if (button == "yes") {
    		                
    		                    var data = [dd];
    		                    // alert(records);
    		                    Ext.Array.each(records, function (record) {
    		                        data.push(record.data);
    		                       // alert( Ext.encode(data));
    		                        // record.commit();// 向store提交修改数据，页面效果
    		                    });
    		                    Ext.Ajax.request({
    		                        url:me.SaveServerUrl,
    		                        params: {
    		                            alterUsers: Ext.encode(data)
    		                            
    		                        },
    		                        method: 'POST',
    		                        timeout: 2000,
    		                        success: function (response, opts) {
    		                        	
    		                            var success = Ext.decode(response.responseText).success;
    		                            // 当后台数据同步成功时
    		                            if (success) {
    		                            	alert(Ext.encode(data));
    		                                Ext.Array.each(records, function (record) {
    		                                	//alert(Ext.encode(record));
    		                                    record.commit(); // 向store提交修改数据，页面效果
    
    		                                });
    		                            } else {
    		                                Ext.MessageBox.show({
    		                                    title: "提示",
    		                                    msg: "数据修改失败!"
    		                                    // icon: Ext.MessageBox.INFO
    		                                });
    		                            }
    		                        }
    		                    });
    		                }
    		            });
    		        }
    		        me.fireEvent('OnSave');
                  }
               },
               { xtype: 'button', x: 570, y: 5,text :me.cancelText,
               handler: function () {
            	   var t =Ext.getCmp('grid').getStore();
                   t.load();
                   me.fireEvent('OnCancel');
               }
               }
          ]
        };
        form.listeners = {   
        };
       return form;
     },

      
     /**
     * 生成列表
     * @private
     * @return {}
     */
     createGrid: function () {
         var me = this;
         var grid = {
             xtype: 'gridpanel',
             region: 'center',
             layout: 'fit',
             id:'grid',
             frame: false, //面板渲染
             sortableColumns: false, //排序是否显示，作用于整个列表
             border:false,
             columns:me.ValueField,
             store: me.gridstore(),
                viewConfig: {
                    emptyText: '没有数据！',
                    loadingText: '获取数据中，请等待...',
                    loadMask: true
                },
               plugins: Ext.create('Ext.grid.plugin.CellEditing', { clicksToEdit: 2 }) //双击修改
        };
        grid.listeners = {
            load: function (store) {
                var index = 0;
                store.each(function (record) {
                if (record.data.column_name == '1') {  //column_name 替换成你的列名， '1' 替换成你的值 
                    selModel.selectRow(index);
                }
                index++;
               });
           }
       };
       return grid;
      },
		/**
		* 注册事件
		* @private
		*/
		addAppEvents: function () {
		    var me = this;
		    me.addEvents('OnSave'); //保存
		    me.addEvents('OnCancel'); //撤销
            
		    
		   // me.fireEvent('OnSavepost');
		},

		/**
		* 组装组件内容
		* @private
		*/
		setAppItems: function () {
		    var me = this;
		    var items = [];
		    //生成查询条件
		    var form = me.createForm();
		    if (form)
		        items.push(form);
		
		    //生成列表
		    var grid = me.createGrid();
		    if (grid)
		        items.push(grid);
		
		    me.items = items;
		},
		/**
		* 初始化组件
		*/
		initComponent: function () {
		    var me = this;
		    //常量设置
		    me.initConstants();
		
		    //注册事件
		    me.addAppEvents();
		
		    //组装组件内容
		    me.setAppItems();
		
		    this.callParent(arguments);
		}
    });