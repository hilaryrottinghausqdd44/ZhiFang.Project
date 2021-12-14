//非构建类--通用型组件或控件--双表数据移动
/**
 * 双表数据移动组件
 * 主要解决两表之间的数据选择。如从所有检验项目中选择一些项目到仪器项目表中。
 * 
 * @param
 * selectType：多选方式 0,为单选 1，为复选,默认为0  --测试用例未完成，配置成功
 * dataServerUrl：后台服务地址 --测试用例未完成，配置成功
 * saveServerUrl：后台保存服务地址 --测试用例未完成，配置成功
 * searchField：查询项匹配数据对象的属性（支持多个） 显支持多个（全列，该属性没实现） 
 * keyField：主键字段,是控件扩展属性  --测试用例未完成，配置成功
 * valueField：值字段 --测试用例未完成，配置成功
 * rootLeft：左表根节点 --测试用例未完成，配置成功
 * rootRight：右表根节点 --测试用例未完成，配置成功
 
 * 
 * 对外公开事件 
 * ondrag 当拖拽移动后触发   --测试用例完成
 * onClick 单击列表记录时触发  --测试用例完成
 * onMove 移动列表记录时触发 --测试用例完成
 * onSave 保存时触发  --测试用例完成
 * ondbClick 双击列表记录时触发 --测试用例完成
 * 
 * 对外公开方法
 * setSelectType 设置多选方式  --测试用例未完成
 * getSelectType 获取多选方式 --测试用例完成
 * save 保存列表对象  --测试用例完成
 * reLoad 重载列表对象 --测试用例完成
 * setCol 设置列表列   --测试用例未完成
 * getCol 获取列表列   --测试用例完成
 * setLeftList 设置左列表  ----测试用例未完成
 * getLeftList 获取左列表  --测试用例完成
 * setRightList 设置右列表  --测试用例未完成
 * getRightList 获取右列表  --测试用例完成
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.DdItems', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.dditems', 
    border: true,
    layout: 'border',
    bodyCls:'bg-white',
    cls:'bg-white',
    margin:4,
    frame:false,
    root:'list',
    simpleSelect:true,
    leftListValue:[],//获取左列表
    rightListValue:[],//获取右列表
    searchField:'L2',//查询项匹配数据对象的属性（支持多个）
    
    rightFieldset:true,//右列表容器开关
    rightFieldsetTitle:'右列表',//右列表容器标题
    leftFieldsetTitle:'左列表',//左列表容器标题名称
    leftFieldset:true,//左列表容器开关
    /**
     * 时间戳，用于修改保存时使用
     * @type String
     */
    DataTimeStamp:'',
    
    width:660,
    height:460,
    
    selectType:true,//多选方式(列表数据是否允许多选)true:允许多选;false:不允许多选
    selType:'',
    leftServerUrl:'',//左列表获取后台数据服务地址
    rightServerUrl:'',//右列表获取后台数据服务地址
    saveServerUrl:'',//保存服务地址

    leftField:[],//左列表的model的Field(内部使用)
    rightField:[],//右列表的model的Field(内部使用)
    
    valueLeftField:[],//数据列表值字段,可以是外面做好数据适配后传进来
    valueRightField:[],//数据列表值字段,可以是外面做好数据适配后传进来
    
    btnLeft:false,//左移按钮显示(false)/隐藏:true
    btnAllLeft:false,//显示(false)/隐藏:true
    btnRight:false,//显示(false)/隐藏:true
    btnAllRight:false,//显示(false)/隐藏:true
    btnHidden:false,//控制(保存/取消按钮)显示:false,隐藏:true
    
    filterLeft:false,//控制左面板过滤栏的显示:false,隐藏:true
    filterRight:false,//控制右面板过滤栏的显示:false,隐藏:true
  
    fieldSetLeft:true,//控制左列表最外层的显示:false,隐藏:true
    fieldSetRight:true,//控制右列表最外层的显示:false,隐藏:true
    
    leftInternalWhere:'',  //左列表的内部hql
    leftExternalWhere:'', //左列表的外部hql
    
    rightInternalWhere:'',  //右列表的内部hql
    rightExternalWhere:'', //右列表的外部hql
    //objecEntity:'',//需要更新数据的数据对象名称
    
    delServerUrl:'',//删除数据服务
    leftPrimaryKey:'',//左列表的字段主键id
    rightPrimaryKey:'',//右列表的字段主键id
    
    leftFilterField:'',//左列表的外部传入的关系表一的过滤字段(外部传入参数,加载左列表数据的过滤条件的字段名称)--如某个检验小组的Id
    leftFilterValue:'',//左列表的外部传入的过滤条件的值
    leftMatchField:'',//左列表与右列表的Id的关系匹配字段
    
    leftObjectName:'',//左列表的数据对象名称
    rightObjectName:'',//右列表的数据对象名称
    
    relationObjectName:'',//关系表一的数据对象(保存数据时调用)
    leftFieldStr:'',//左列表前台显示的字段(内部使用)
    rightFieldStr:'',//右列表前台显示的字段(内部使用)
    
    leftDataTimeStampValue:'',//关系表一的数据对象时间戳值
    
    setLeftFilterValue:function(value){
        var me=this;
        me.leftFilterValue=value;
        return me.leftFilterValue;
    },
    setleftDataTimeStampValue:function(value){
        var me=this;
        me.leftDataTimeStampValue=value;
        return me.leftDataTimeStampValue;
    },
     /**
     * setTitle 获取组件标题
     * @param {} title
     */
    getTitle:function(title){
        var me=this;
       return me.title;
    },
   /**
     * 设置组件宽度
     * @param {} width
     */
    setWidth:function(width){
        var me=this;
        return me.setSize(width);
    },
    /**
     * 返回组件宽度
     * @return {}
     */
    getWidth:function(){
        var me=this;
        return me.width;
    },
    /**
     * 设置组件高度
     * @param {} height
     */
    setHeight:function(height){
        var me=this;
        return me.setSize(undefined, height);
    },
    /**
     * 返回组件高度
     * @return {}
     */
    getHeight:function(){
        var me=this;
        return me.height;
    },
    /**
     * 解析左列表处理传列表值字段,
     * 封装store数据的Field数组
     * 
     */
     setLeftField: function () {
         var me = this;
         if (me.valueLeftField.length > 0) {
             me.leftFieldStr='';
             me.leftField=[];
             for (var i = 0; i < me.valueLeftField.length; i++) {
                 var test = me.valueLeftField[i].dataIndex;
                 if(i== me.valueLeftField.length-1){
                    me.leftFieldStr=me.leftFieldStr+test;
                 }else{
                    me.leftFieldStr=me.leftFieldStr+(test+',');
                 }
                 me.leftField.push(test);
             }
             
         }
     },
    /**
     * 解析右列表处理传列表值字段,
     * 封装store数据的Field数组
     * 
     */
     setRightField: function () {
         var me = this;
         if (me.valueRightField.length > 0) {
             me.rightFieldStr='';
             me.rightField=[];
             for (var i = 0; i < me.valueRightField.length; i++) {
                 var test = me.valueRightField[i].dataIndex;
                 if(i== me.valueRightField.length-1){
                    me.rightFieldStr=me.rightFieldStr+test;
                 }else{
                    me.rightFieldStr=me.rightFieldStr+(test+',');
                 }
                 me.rightField.push(test);
             }
         }
     },
	/***
	 * 将传入的字符串转换为hql查询格式
	 * 第一个数据对象的字母为小写,其他的数据对象不变,
	 * @param {} objectName
	 * @return {}
	 */
    transformHqlStr: function (objectName) {
        var me = this;
        var defaultValueArr=objectName.split('_');
        var tempStr='';
        for(var j=0;j<defaultValueArr.length-1;j++){
            if(j==0){
                var tempVlue=defaultValueArr[j];
                tempStr=tempStr+tempVlue.toLowerCase()+'.';
            }
            else if(j<defaultValueArr.length-1){
                tempStr=tempStr+defaultValueArr[j]+'.';
            }
        }
        var hqlStr =tempStr+defaultValueArr[defaultValueArr.length-1];
        return hqlStr;
     },
    /**
     * 将右列表选择中的行数据信息保存到左列表的数据库中
     * @author hujie eidt 2013-06-08
     * @private
     * @param {} obj
     * @param {} callback
     */
    saveToServer:function(obj,callback){
        var me = this;
        var myUrl =me.saveServerUrl;
        //Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            //params:obj,
            params:Ext.JSON.encode(obj),
            method:'POST',
            //timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                }else{
                    Ext.Msg.alert('提示','保存失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','保存请求失败！');
            }
        });
    },

    /**
     * 根据ID删除数据
     * @private
     * @param {} id
     * @param {} callback
     */
    deleteInfo:function(id2,callback){
        var me = this;
        var myUrl = ""+me.delServerUrl+"?id="+id2;
        Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                }else{
                    Ext.Msg.alert('提示','删除信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
            },
            failure:function(response,options){ 
                Ext.Msg.alert('提示','删除信息请求失败！');
            }
        });
    },

     /***
      * 重新加载右列表数据
      * @param {} where
      */
     loadR:function(){
        var me=this;
        var rightGrid=me.getRightGridPanel();
        rightGrid.store.reload();
     },
     /***
      * 重新加载左列表数据
      * @param {} where
      */
     loadL:function(){
        var me=this;
        var leftGrid=me.getLeftGridPanel();
	    leftGrid.store.reload();
     },
    /**
 	 * 创建服务代理
 	 * @private
 	 * @param {} url
 	 * @return {}
 	 */
 	createProxy:function(url){
 		var proxy = {
             type:'ajax',
             url:url,
             reader:{
             	type:'json',
             	root:'list',
             	totalProperty:'count'
             },
             extractResponseData: function(response){
             	var data = Ext.JSON.decode(response.responseText);
             	if(data.ResultDataValue && data.ResultDataValue != ""){
             		var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
 	            	data.count = ResultDataValue.count;
 	            	data.list = ResultDataValue.list;
             	}else{
 	            	data.count = 0;
             		data.list = [];
             	}
             	response.responseText = Ext.JSON.encode(data);
             	return response;
           	}
         };
         return proxy;
 	},

    /**
     * 创建右列表对象数据源
     * 未实现数据项适配的功能
     * 
     */
    rightStore:function(where){
        var me = this;
        var myUrl=me.getRightUrl(where);
        //数据代理
        var proxy = me.createProxy(myUrl);
        var obj = {
            fields:me.rightField,
            proxy:proxy
        };
        var store = Ext.create('Ext.data.Store',obj);
        
        if(store){
            store.load();//加载数据
        }else{
            Ext.Msg.alert("提示","加载右列表数据失败！");
        }
        return store;
   },
   getRightUrl:function(where){
      var me=this;
      me.setRightField();
      var myUrl ='';
        //前台需要显示的字段
        var fields = me.rightFieldStr;
        if(me.rightServerUrl == ""){
            Ext.Msg.alert("提示","没有配置右列表的获取数据的服务！");
        }
        
        if(!fields){
            fields = "";
        }
        myUrl = me.rightServerUrl+ "?isPlanish=true&fields=" + fields;
        
        //服务地址
        me.rightExternalWhere=where;
        var w=''; 
        if(me.rightInternalWhere){ 
            w+=me.rightInternalWhere;
        }
        if(where&&where!=''){
            if(w!=''){
                w+=' and '+where;
            }else{
                w+=where;
            }
        }
        myUrl = myUrl+'&where='+w;
        return myUrl;
   },
   getLeftUrl:function(where){
		var me=this;
		me.setLeftField();
		//前台需要显示的字段
		var fields =me.leftFieldStr;
		if(me.leftServerUrl == ""){
		    Ext.Msg.alert("提示","没有配置左列表的获取数据的服务！");
		}
		
		if(!fields){
		    fields = "";
		}
		var myUrl = me.leftServerUrl+ '?isPlanish=true&fields=' + fields;
		
		//服务地址
		me.leftExternalWhere=where;
		var w=''; 
		if(me.leftInternalWhere){ 
		    w=w+me.leftInternalWhere;
		}
        
		if(where&&where!=''){
		    if(w!=''){
		        w=w+(' and '+where);
		    }else{
		        w+=where;
		    }
		}
       myUrl =myUrl+'&where='+w;//左列表获取后台数据服务地址
       return myUrl;
     },
    /**
      * 创建左列表对象数据源
      * 未实现数据项适配的功能
      * 
      */
 	leftStore:function(where){
        var me=this;
        var w='';
        var store = null;
        if(me.leftFilterField&&me.leftFilterField!=''&&me.leftFilterValue!=''){ 
              var tempStr=me.transformHqlStr(me.leftFilterField);
              var tempStr2=(tempStr+'='+me.leftFilterValue);
              w=w+tempStr2;
              if(where&&where!=''){
                    w=w+(' and '+where);
               }
         }
         if(me.leftFilterValue==''){
            w='';
            w=w+' 1!=1 ';
         }
            var myUrl=me.getLeftUrl(w);
            //数据代理
            var proxy = me.createProxy(myUrl);
            var obj = {
                fields:me.leftField,
                proxy:proxy
            };
            store = Ext.create('Ext.data.Store',obj);
            
            if(store){
                store.load();//加载数据
            }else{
                Ext.Msg.alert('提示','加载左列表数据失败!');
            }
		return store;
	},
    /***
     * 重新加载左列表数据
     * @param {} where
     */
     leftLoad:function(params){
        var me=this;
          var w='';
          if(params&&params!=''){
            me.leftFilterValue=params;
          }
          if(me.leftFilterField&&me.leftFilterField!=''&&me.leftFilterValue!=''){ 
              var tempStr=me.transformHqlStr(me.leftFilterField);
              var tempStr2=(tempStr+'='+me.leftFilterValue);
              w=w+tempStr2;
              var myUrl=me.getLeftUrl(w);
		      var leftGrid=me.getLeftGridPanel();
		      if(leftGrid){
		          leftGrid.store.proxy.url=myUrl; 
		          leftGrid.store.load();
		      }
          }
        
     },
    /***
     * 重新加载右列表数据
     * @param {} where
     */
     rightLoad:function(whereStr){
          var me=this;
          var w='';
          //过滤左列表的数据
          var valueStr=me.getleftItemsForServer('');
          if(valueStr!=''){
            var rightPrimaryKey2=me.transformHqlStr(me.rightPrimaryKey);
            w=w+rightPrimaryKey2+' not in '+valueStr;
          }
          //外部where串
          if(whereStr&&whereStr!=''){
            w=w+whereStr;
          }
          var myUrl=me.getRightUrl(w);
          var rightGrid=me.getRightGridPanel();
          if(rightGrid){
              rightGrid.store.proxy.url=myUrl; 
              rightGrid.store.load();
          }
        
     },
     /***
      * 获取左列表的某个分类的所有数据,供右列表作过滤
      * @return {}
      */
    getleftItemsForServer:function(params){
        var me = this;
        var w='';
          if(params&&params!=''){
            me.leftFilterValue=params;
          }
          if(me.leftFilterField&&me.leftFilterField!=''&&me.leftFilterValue!=''){ 
              var tempStr=me.transformHqlStr(me.leftFilterField);
              var tempStr2=(tempStr+'='+me.leftFilterValue);
              w=w+tempStr2;
          }else{
                return '';
          }
        var myUrl=me.getLeftUrl(w);
        if(myUrl==""||myUrl==null){
            Ext.Msg.alert('提示','没有配置数据服务地址或者配置失败！');
            return '';
        }
        var tempStr='';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = {count:0,list:[]};
                if(result["ResultDataValue"] && result["ResultDataValue"] != ""){
                    ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                }
                var count = ResultDataValue['count'];
	                if(count>0){
		                tempStr =tempStr+' (';
		                for (var i = 0; i <count; i++) { 
				           tempStr =tempStr+(ResultDataValue.list[i][me.leftMatchField]+',');
				           
		                }
	                   tempStr=tempStr.substring(0,tempStr.length-1);
	                   tempStr =tempStr+' )';
	                }else
	                {
	                    tempStr='';
	                }
                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        return tempStr;
    },
     //设置多选方式
     setSelectType: function (value) {
         var me = this;
         var y =me.getLeftGridPanel();
         y.selType=value;
         return y.selType;
         var y1 =me.getRightGridPanel();
         y1.selType=value;
         return y1.selType;
     },
     //获取多选方式
     getSelectType: function () {
         var me = this;
         return me.selectType;//复选框
     },
     //重载列表对象
     reLoad:function()
     { 
    	 var me = this;
    	 var t =me.getLeftGridPanel().getStore();
    	 var t1 =me.getRightGridPanel().getStore();
    	 t.leftLoad('');
    	 t1.rightLoad('');
     },
     //设置列表列
     setCol:function(value){
         var me = this;
         var t =me.getLeftGridPanel();
    	 var t1 =me.getRightGridPanel();
         t.valueField =value;
         t1.valueField=value;
     },
     //获取列表列
     getCol:function(){
    	 var me = this;
    	 var t =me.getLeftGridPanel();
    	 t.valueField =me.valueField;
         return t.valueField ;
    	 var t1 =me.getRightGridPanel();
    	 t1.valueField =me.valueField;
    	 return  t1.valueField;
     },
     //设置左列表
     setLeftList:function(value){
         var me = this;
    	 me.leftListValue=value;
    	 return me.leftListValue;
     },
     //获取左列表的某一列的所有数据
     getLeftleftMatchFieldValueList:function(){
         var me = this;
         var leftGrid =me.getLeftGridPanel();
         var store =leftGrid.getStore();
         var tempStr =''; 
         if(store!=null&&store.getCount()>0){
            tempStr =' (';
          store.each(function(record){
              tempStr =tempStr+(record.data[me.leftMatchField]+',');
          });
           tempStr=tempStr.substring(0,tempStr.length-1);
           tempStr =tempStr+' )';
         }
          return tempStr;
          
     },
     //获取左列表所有数据
     getLeftList:function(){
    	 var me = this;
    	 var t =me.getLeftGridPanel();
    	 var modified =t.getStore().getRange(0,t.getStore().modified);
     	 var json = []; 
          Ext.each(modified, function(item){ 
        	  json.push(item.data); 
          });
          return json;
          
     },
     //设置右列表
     setRightList:function(value){
    	 var me = this;
     	 me.rightListValue=value;
     },
     //获取右列表所有数据
     getRightList:function(){
    	 var me = this;
    	 var t =me.getRightGridPanel();
    	 var modified =t.getStore().getRange(0,t.getStore().modified);
     	 var json = []; 
         Ext.each(modified, function(item){ 
        	 json.push(item.data); 
         });
         return json;
     },
     /***
      * 右列表单行信息左移到左列表
      * @return {}
      */
     createBtnLeft:function(){
        var me=this;
        var com={
              xtype: 'button',text: '<',margins: '5',width:40,
              hidden:me.btnLeft,
                handler: function(){
                    me.fireEvent('onbtnLeftClick');
                    me.addLeftRow(null);
                    me.leftLoad('');
                    me.rightLoad('');
                }
            };
         return com;
     },
     /***
      * 右列表所有行信息左移到左列表
      * @return {}
      */
     createBtnAllLeft:function(){
        var me=this;
        var com={
             xtype: 'button',text: '<<', margins: '5',width:40,
             hidden:me.btnAllLeft,
             handler: function(){
                me.fireEvent('onbtnAllLeftClick');
                me.addAllLeftRow();
                me.leftLoad('');
                me.rightLoad('');
             }         
           };
         return com;
     },
   /***
    * 删除左列表选中的行数据
    */
    removeLeftRow:function(record){
        var me = this;
        var grid=me.getLeftGridPanel();
        var rows = grid.getSelectionModel().getSelection(); 
        if(record!=null){
            rows = record;
        }else{
            rows = grid.getSelectionModel().getSelection();
        }
        Ext.Array.each(rows,function(record){
            var id = record.get(me.leftPrimaryKey);
            me.deleteInfo(id,null);
        });
        me.leftLoad('');
        me.rightLoad(''); 
    },
   /***
    * 删除左列表选中的行数据
    */
    removeAllLeftRow:function(){
        var me = this;
        var grid=me.getLeftGridPanel();
        var rows = grid.store.data.items;
        Ext.Array.each(rows,function(record){
            var id = record.get(me.leftPrimaryKey);
            me.deleteInfo(id,null);
        });
        me.leftLoad('');
        me.rightLoad(''); 
    },
     /***
      * 往左列表添加行信息
      */
     addLeftRow:function(record){
        var me=this;
        var rightGrid=me.getRightGridPanel();
        var rows = rightGrid.getSelectionModel().getSelection();
        if(record!=null){
            rows = record;
        }else{
            rows = rightGrid.getSelectionModel().getSelection();
        }
        var leftGrid = me.getLeftGridPanel();
	    //查询后台数据,该记录是否已经存在左列表中,查询条件是左列表的过滤字段和新合成的查询字段
        var selectField=me.transformHqlStr(me.leftMatchField);
        //遍历右列表的选择的行记录
         Ext.Array.each(rows,function(record){
            var whereStr='';
            whereStr=whereStr+(me.transformHqlStr(me.leftFilterField)+'='+me.leftFilterValue);
            var rightId = record.get(me.rightPrimaryKey);
            //where串
            whereStr=whereStr+(' and '+selectField+'='+rightId);
            var tempTtore=me.leftStore(whereStr);
            if(tempTtore&&tempTtore!=null&&tempTtore.data.length==0){
            //生成新增数据所需的数据
            var leftFilterValueTemp=me.leftFilterValue;
            if(leftFilterValueTemp==''||leftFilterValueTemp==null){
                Ext.Msg.alert('提示','不能保存,没有传入外部过滤条件(leftFilterValue)的值');
                return;
            }else{
                me.save(record);
            }
            }
        });

     },
     save:function(record){
        var me=this;
	    //新增到数据库
	    var id =-1;
	    var labID =0;
        var rightId = record.get(me.rightPrimaryKey);
	    //生成新增数据所需的数据
	    var leftFilterValueTemp=me.leftFilterValue;
        var rightStr=''+me.rightObjectName+'_'+'DataTimeStamp';//关系表右列表的时间戳
        var rightDataTimeStamp=record.get(rightStr);
        var rightArr=[];
        if(rightDataTimeStamp&&rightDataTimeStamp!=undefined){
            rightArr=rightDataTimeStamp.split(',');
        }else{
            Ext.Msg.alert('提示','不能保存,构建时没有选择右列表的数据对象的时间戳列');
            return;
        }
        var leftDataTimeStamp=''+me.leftDataTimeStampValue;
        var leftArr=[];
        if(leftDataTimeStamp&&leftDataTimeStamp!=undefined){
            leftArr=leftDataTimeStamp.split(',');
        }else{
            leftArr=rightArr;
        }
        var leftJson={Id:leftFilterValueTemp,DataTimeStamp:leftArr};
        var rightJson={Id:rightId,DataTimeStamp:rightArr};
        var relationObjectName="'"+me.relationObjectName+"'";
        var rightObjectName="'"+me.rightObjectName+"'";
        var  newAdd= {
            LabID:labID,//左列表的新增行记录的主键ID
            Id:id//左列表的新增行记录的主键ID
            //me.relationObjectName:leftJson,//左列表的关系一主键id
            //关系表二的数据对象
            //rightObjectName:rightJson//左列表的关系二主键id
        };
        newAdd[me.relationObjectName]=leftJson;
        newAdd[me.rightObjectName]=rightJson;
        var entity={"entity":newAdd};
        me.saveToServer(entity,null);
     },
     /***
      * 往左列表添加行信息
      */
     addAllLeftRow:function(){
        var me=this;
        var rightGrid=me.getRightGridPanel();
        var rows = rightGrid.store.data.items;
        var leftGrid = me.getLeftGridPanel();
        //查询后台数据,该记录是否已经存在左列表中,查询条件是左列表的过滤字段和新合成的查询字段
        var selectField=me.transformHqlStr(me.leftMatchField);
        //遍历右列表的选择的行记录
         Ext.Array.each(rows,function(record){
            var whereStr='';
            whereStr=whereStr+(me.transformHqlStr(me.leftFilterField)+'='+me.leftFilterValue);
            var rightId = record.get(me.rightPrimaryKey);
            //where串
            whereStr=whereStr+(" and "+selectField+'='+rightId);
            var tempTtore=me.leftStore(whereStr);
            if(tempTtore&&tempTtore!=null&&tempTtore.data.length==0){
            var leftFilterValueTemp=me.leftFilterValue;
            if(leftFilterValueTemp==''||leftFilterValueTemp==null){
                Ext.Msg.alert('提示','不能保存,没有指定左列表的外部传入的过滤条件的值');
                return;
            }else{
                me.save(record);
            }
            }
        });
        
     },
     /***
      * 将左列表与右列表的匹配字段的列信息转化成where串,以供过滤右列表的数据
      * @return {}
      */
     getleftMatchFieldValue:function(){
        var me=this;
        //过滤右列表的数据,不显示左列表已经存在的行记录信息
        var grid=me.getLeftGridPanel();
        var items = grid.store.data.items;
        var tempStr='';
        Ext.Array.each(items,function(record){
            var tempValue=record.get(me.leftMatchField);
            if(tempValue!=''&&tempValue!=null){
                tempStr=tempStr+tempValue+',';
            }
        });
       if(tempStr.length>0){
            tempStr=tempStr.substring(0,tempStr.length-1);
            tempStr='('+tempStr+')';
            var rightPrimaryKey2=me.transformHqlStr(me.rightPrimaryKey);
            tempStr=" "+rightPrimaryKey2+' not in '+tempStr;
       }else{
            tempStr='';
       }
       return tempStr;
     },
     /***
      * 左列表单行信息移出列表
      * @return {}
      */
     createBtnRight:function(){
        var me=this;
        var com={
            xtype: 'button',height: 20,text: '>',margins: '5',width:40,
            hidden:me.btnRight,
            handler: function(){
                me.removeLeftRow(null);
                me.fireEvent('onbtnRightClick');
            }
            };
         return com;
     },
     /***
      * 左列表所有行信息移出列表
      * @return {}
      */
     createBtnAllRight:function(){
        var me=this;
        var com={
            xtype: 'button',text: '>>',margins: '5',width:40,
            hidden:me.btnAllRight,
            handler: function(){
                me.removeAllLeftRow();
                me.fireEvent('onbtnAllRightClick');
                }
            };
         return com;
     },
     /***
      * 创建左过滤栏
      */
     createDockedItemsFilterL:function(){
        var me=this;
        var com='';
        if(me.filterLeft==false){
        com={
           xtype: 'toolbar',dock: 'bottom', bodyCls:'bg-white',
           cls:'bg-white',
           items: [
               {xtype: 'textfield', fieldLabel: '按过滤',width:230,labelAlign: 'right',height:25,
               itemId:'com1',enableKeyEvents:true,
                queryMode: 'local',
                displayField:me.searchField,
                listeners:{
                 change:function(textfield, newValue,oldValue, eOpts ){
                       me.filterFn(newValue);
                   },
                   keyup:function(e, eOpts ){
                       var grid=me.getLeftGridPanel();
                       var store=grid.getStore();
                   }
                }
            }]
         };
        }else{
            com='';
        }
       return com;
     },
     
     /***
      * 创建右过滤栏
      */
     createdockedItemsFilterR:function(){
        var me=this;
        var com='';
        if(me.filterRight==false){
	        com={
	            xtype: 'toolbar',
	            dock: 'bottom', 
	            bodyCls:'bg-white',
	            cls:'bg-white',
                items: [
                    {xtype: 'textfield', fieldLabel: '按过滤',width:230,
                    labelAlign: 'right',height:25,
                    displayField:me.searchField,enableKeyEvents:true,
                    queryMode: 'local',itemId:'filterRightCom',
                    listeners:{
                        change:function(textfield, newValue,oldValue, eOpts ){
                            me.filterFnright(newValue);
                        },
                        keyup:function(e, eOpts ){
                            var grid=me.getRightGridPanel();
                            var store=grid.getStore();
                        }
                    }
                  }
              ]
	           };
        }else{
            com='';
        }
       return com;
     },
    /**
	 * 常量设置
	 * @private
	 */
	initConstants:function(){
		var me = this;
    },
    /**
	 * 生成左列表
	 * @private
	 * @return {}
	 */
    createLeft:function(){
    	var me = this;
        if(me.valueLeftField.length==0){
            Ext.Msg.alert("左列表没有配置或匹配失败");
            return null;
        }
        var selType='';
        var simpleSelect=false;
        if(me.selectType==false)
        {
            selType='checkboxmodel';//复选框
            simpleSelect=true;
           
        }else if(me.selectType==true){
            selType='rowmodel';
            simpleSelect=false;
          
        }
    	var left = {
    		xtype: 'gridpanel',
    		width:300,
            height: me.height,
    		title: '',
    		itemId:'leftGrid',
            padding:'0,0,0,0',
    		selType:selType,//复选框
    		columns:me.valueLeftField,
         	multiSelect:true,//允许多选
            simpleSelect: simpleSelect,    //简单选择功能开启 
            enableKeyNav: true,     //启用键盘导航 
         	store:me.leftStore(''),//获得左列表,
         	dockedItems:me.createDockedItemsFilterL(),
            multiSelect: true,
            stripeRows: true,
            viewConfig: {
                plugins: {
                     ptype: 'gridviewdragdrop',
                     dragGroup: 'firstGridDDGroup',
                     dropGroup: 'secondGridDDGroup'
                },
                listeners:{
                	drop: function(node, data, dropRec, dropPosition) {
                	    var dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('name') : ' on empty view'; 
                	    me.fireEvent('onLeftDrop');
                    },
                    beforedrop:function(node,data,overModel,dropPosition,dropFunction,eOpts ){
                    	 me.fireEvent('onMove');
                    }
                }
    	    }
    	};
    	left.listeners = { 
            itemdblclick:function(grid,record,item,index,e,eOpts){
                me.removeLeftRow(record);
        	    me.fireEvent('onLeftdbClick');
            },
            itemclick:function(grid,record,item,index,e,eOpts){
                
            	me.fireEvent('onLeftClick');
            	return;
            },
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
                //列表宽度和高度赋值
                me.fireEvent('onLeftResizeClick');
            },
            columnresize:{//列宽度改变
                fn: function(ct,column,width,e,eOpts){
                    me.fireEvent('onLeftColumnResizeClick');
                }
            },
            columnmove:{//列位置移动
                fn: function(ct,column,fromIdx,toIdx,eOpts){
                    me.fireEvent('onLeftColumnMoveClick');
                }
            }
    	};
    	return left;
    },
   
    /**
	 * 生成右边列表
	 * @private
	 * @return {}
	 */
    createRight:function(){
    	var me = this;
        if(me.valueRightField.length==0){
            Ext.Msg.alert("右列表没有配置或匹配失败");
            return null;
        }
        var selType='';
        var simpleSelect=false;
        if(me.selectType==false)
        {
            selType='checkboxmodel';//复选框
            simpleSelect=true;
           
        }else if(me.selectType==true){
            selType='rowmodel';
            simpleSelect=false;
          
        }
    	var right = {
    		xtype: 'gridpanel',
    	    width: 300,
            height: me.height,
    	    title: '',
            padding:'0,0,0,0',
    	    itemId:'rightGrid',
    	    columns:me.valueRightField,
            selType:selType,//复选框
         	multiSelect:true,//允许多选
            simpleSelect:simpleSelect,    //简单选择功能开启 
            enableKeyNav: true,     //启用键盘导航 
            store:me.rightStore(''),//获得右列表
            dockedItems:me.createdockedItemsFilterR(),
            multiSelect: true,
            stripeRows: true,
            viewConfig: {
               plugins: {
               ptype: 'gridviewdragdrop',
               dragGroup: 'secondGridDDGroup',
               dropGroup: 'firstGridDDGroup'
         	   },
         	    listeners:{
               	    drop: function(node, data, dropRec, dropPosition) {
         		        var dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('name') : ' on empty view';
                        me.fireEvent('onRightDrop');
                    },
                    beforedrop:function(node,data,overModel,dropPosition,dropFunction,eOpts ){
                    	 me.fireEvent('onMove');
                    }
               }
    	   }
        };
    	right.listeners = { 
    		itemclick:function(){
    		    me.fireEvent('onRightClick');
    	    },
    	    itemdblclick:function(grid,record,item,index,e,eOpts){
        	   me.fireEvent('onRightdbClick');
               me.addLeftRow(record);
               me.leftLoad('');
               me.rightLoad('');
          },
            resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
                //列表宽度和高度赋值
                me.fireEvent('onRightResizeClick');
            },
            columnresize:{//列宽度改变
                fn: function(ct,column,width,e,eOpts){
                    me.fireEvent('onRightColumnResizeClick');
                }
            },
            columnmove:{//列位置移动
                fn: function(ct,column,fromIdx,toIdx,eOpts){
                    me.fireEvent('onRightColumnMoveClick');
                }
            }
    	};
    	return right;
    },  

    /**
     * 生成按钮组
     * @private 
     * @return {}
     */
    createButtonArr:function(){
    	var me = this;
        var btnItems=[];
        btnItems.push(me.createBtnLeft());
        btnItems.push(me.createBtnAllLeft());
        btnItems.push(me.createBtnRight());
        btnItems.push(me.createBtnAllRight());
    	var button = {
    		xtype: 'panel',
    		height: 443,
    		width: 60,
    	    region: 'west',
    		title: '',
    		border:false,
    		  layout: {
    	        align: 'center',
    	        pack: 'center',
    	        type: 'vbox'
    	    },
    		items:btnItems
    	};
    	return button;
    },

    /**
     * 生成btton
     * @private 
     * @return {}
     */
    createbutton1:function(){
    	var me = this;
    	var button = {
    		xtype: 'panel',
    		height: 30,
            width:me.width-20,
            hidden:me.btnHidden,
    	    region: 'south',
    	    itemId:'buttonControl',
    		title: '',
    		border:true,
    		layout: {
                align: 'middle',
                pack: 'center',//'end',
                type: 'hbox'
            },
    		items: [
		        {xtype: 'button',text: '确定',iconCls: 'Save', margin:5,iconCls:'build-button-save',
	            	handler: function(){
	            	  me.fireEvent('onSaveClick');
	                }
	            },	
	            {xtype: 'button',text: '取消',iconCls:'build-button-refresh',
	             handler: function(){
                    me.fireEvent('onCancelClick');
	                }
	            }
	        
            ]   
    	};
    	return button;
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
    	var me = this;
    	var items = [];
    	//左列表
    	var left = me.createLeft();
    	if(left){
            var leftCom=me.createLeftFieldset(left); 
    		items.push(leftCom);
        }
    	//按钮
    	var button = me.createButtonArr();
    	if(button)
    		items.push(button);
     	//右列表
    	var right = me.createRight();
    	if(right){
            var rightCom=me.createRightFieldset(right); 
    		items.push(rightCom);
        }
    		//确定和重置
        var button= me.createbutton1();
        if(button)
            items.push(button);
    	
    	me.items = items;
    },
    createLeftFieldset:function(leftItem){
	    var me=this;
        var border=0;
        var fieldsetTitle='';
        if(me.leftFieldset==true){
            border=1;
            fieldsetTitle=me.leftFieldsetTitle;
        }else{
            border=0;
            fieldsetTitle='';
        }
	    var com= 
	        {
	        xtype:'fieldset',
            region: 'west',
            height: me.height,
            padding:'2,2,2,2',
            layout:'fit',
            border:border,
	        title:fieldsetTitle,
	        collapsible: false,
            itemId:'leftFieldset',
	        items :[leftItem]
	        }
	     return com;
    },
   createRightFieldset:function(rightItem){
        var me=this;
        var border=0;
        var fieldsetTitle='';
        if(me.rightFieldset==true){
            border=1;
            fieldsetTitle=me.rightFieldsetTitle;
        }else{
            border=0;
            fieldsetTitle='';
        }
        var com= 
            {
            xtype:'fieldset',
            region: 'center',
            layout:'fit',
            height: me.height,
            itemId:'rightFieldset',
            padding:'2,2,2,2',
            border:border,
            title:fieldsetTitle,
            //collapsible: false,
            items :[rightItem]
            }
         return com;
    },
    /**
     * 注册事件
     * @private
     */
     addAppEvents: function () {
         var me = this;
         me.addEvents('onLeftDrop'); //当拖拽左列表移动后触发
         me.addEvents('onLeftdbClick');//左列表行双击后
         me.addEvents('onRightDrop'); //当拖拽左列表移动后触发
         me.addEvents('onRightdbClick'); //右列表行双击后
         me.addEvents('onLeftClick'); //左列表行单击后
         me.addEvents('onRightClick'); //右列表行单击后
         me.addEvents('onSaveClick'); //单击确定按钮时触发
         me.fireEvent('onCancelClick');
         
         me.addEvents('onbtnLeftClick');
         me.addEvents('onbtnRightClick'); //
         
         me.addEvents('onbtnAllLeftClick');
         me.addEvents('onbtnAllRightClick'); //
     },
    /**
     * 初始化组件
     */
    initComponent:function(){
    	var me = this;
    	//常量设置
    	me.initConstants();
    	//注册事件
		me.addAppEvents();
    	//组装组件内容
    	me.setAppItems();
        
    	this.callParent(arguments);
        
    },
    afterRender:function(){
        var me = this;
        me.callParent(arguments);
        me.rightLoad('');
        if(Ext.typeOf(me.callback)=='function'){me.callback(me);}
    },
    /**
     * 模糊查询过滤函数(左）
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         var grid = me.getLeftGridPanel();
         var store = grid.getStore(); //reload()
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record, id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     },
     getRightGridPanel: function () {
        var me=this;
        var rightGrid=me.getComponent('rightFieldset').getComponent('rightGrid');
        return rightGrid;
     },
     getLeftGridPanel: function () {
        var me=this;
        var com2=me.getComponent('leftFieldset').getComponent('leftGrid');
        return com2;
     },
    /**
     * 模糊查询过滤函数(右）
     * @param {} value
     */
     filterFnright: function (value) {
         var me = this, valtemp = value;
         var grid = me.getRightGridPanel();
         var store = grid.getStore(); //reload()
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(" ");
         store.filterBy(function (record, id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     }
    	 
});