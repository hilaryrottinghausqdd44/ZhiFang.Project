//非构建类--通用型组件或控件--文字属性设置(可以设置字体风格,字体大小,字体类型,字体颜色,字体下划线/字体删除线)
/**
 * 修改完成--测试用例未完成
 * 
 * 对外公开属性
 *  title: //面板窗口标题设置,默认值为''
 *  border : false,//边框线显示 true,或隐藏false
 *  fontStyle://字体风格,默认值为 "font-style:\'normal\';" 
 *  fontSize://字体大小,默认值为 "font-size:\'12px\';" 
 *  fontFamily://字体类型,默认值为 "font-family:\'SimSun\';" 
 *  colorValue://字体颜色值,默认值为 "color:\'#000000\';" 
 *  textDecoration://字体下划线/字体删除线,默认值为'' 
 *  width: //控件高度,默认值为460 
 *  height: //控件宽度,默认值为270
 *  bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
 *  cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
 *  labelcls:'labelcls',//||{fontSize:'16px',color:'black',fontWeight:'bold'},
 *  //'labelcls'为"css/icon.css"里字体属性设置:label样式
 *  btnHidden: false,//确定或者取消按钮的显示false或者隐藏true  
 *  
 * 对外公开事件
 * onOKCilck 确定事件
 * onCancelCilck 取消事件
 * 
 * 对外公开方法
 * GetValue() //获取设置当前控件的文字属性结果值,
 * 如"{font-style:'normal';font-size:'19px';font-family:'SimSun';color:'#993366';}"
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度
 * setTitle 设置组件标题
 * getTitle 返回组件标题
 * getValue 返回组件结果值
 * SetInitValue 
 * 
 */

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.FontStyleSet', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.fontstyleset',
    layout:'absolute',
    frame:true,
    border : false,//边框线显示 true,或隐藏false
    padding:0,
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    labelcls:'labelcls',//||{fontSize:'16px',color:'black',fontWeight:'bold'},//为"css/icon.css"里字体属性设置:label样式
    style :'',
    title: '',//面板窗口标题名称设置
    titleAlign:'center',//面板窗口标题名称显示位置设置
    width:460,//控件宽度
    height:270,//控件高度
    fontStyle:"font-style:normal;",//字体风格
    fontSize:"font-size:12px;",//字体大小
    fontFamily:"font-family:SimSun;",//字体类型
    colorValue:"color:#000000;",//字体颜色值
    textDecoration:'',//字体下划线/字体删除线
    font:'',//最终的样式字符串,如 "{fontSize:'16px',color:'green',fontWeight:'bold'}"
  
    btnHidden: false,//确定或者取消按钮的显示false或者隐藏true
   
    win:null,//创建和弹出创建颜色选择器
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
     * 返回结果值
     * @return {}
     */
    getValue:function(){
        var me=this;
        var lastValue="";
        if(me.fontStyle!=null||me.fontStyle!="")
        {lastValue=lastValue+me.fontStyle}
        if(me.fontSize!=null||me.fontSize!="")
        {lastValue=lastValue+me.fontSize}
        if(me.fontFamily!=null||me.fontFamily!="")
        {lastValue=lastValue+me.fontFamily}
        if(me.colorValue!=null||me.colorValue!="")
        {lastValue=lastValue+me.colorValue}
        if(me.textDecoration!=null||me.textDecoration!="")
        {lastValue=lastValue+me.textDecoration}
        lastValue=lastValue+"";
        return me.font=lastValue;//
    },
     /**
     * 将当前控件的文字属性以字符串传入,解析并初始化当前文字属性窗口的值
     * font:font-style:normal;font-size:12px;font-family:SimSun;color:#FF0000;
     * @return {}
     */
    SetInitValue:function(font){
	   var me = this;
	   var test=font;
	   if(test.length>0){
	   var temp1=test.split(";")
	   for(var i=0;i<temp1.length;i++){
	   var temp2=temp1[i].split(":");
	     switch(temp2[0])
	                {
	                case "font-style"://字体风格
	                var sf=temp2[1].replace("'","");
	                sf=sf.replace("'","");
	                var sf2="font-style:"+"\'"+ sf +"\';";
                    var sf3="font-style:"+ sf +";";
	                me.getComponent("myFontStyleSetfontStyle_id").setValue(sf2);
	                Ext.fly("lblExample").setStyle("font-style",sf);
	                me.fontStyle=sf3;
	                 break;
	                case "font-size"://字体大小
	                var fontsize1=temp2[1].replace("px","");
	                var fontsize2=fontsize1.replace("'","");
	                fontsize2=fontsize2.replace("'","");
	                 me.getComponent("myFontStyleSetfontSize_id").setValue(fontsize2);
	                me.setFontSize(fontsize2);
	                me.fontSize="font-size:"+fontsize2+"px;"
	                 break;
	                case "font-family"://字体(风格)类型
	                var ff=temp2[1].replace("'","");
	                ff=ff.replace("'","");
	                 me.getComponent("myFontStyleSetfontFamily_id").setValue(ff);
	                me.fontFamily="font-family:"+ff+";";
	                 break;
	                case "color"://颜色
	                me.colorValue=temp2[1];
	                var c4=temp2[1].replace("'","");
	                var c5=c4.replace("'","");
	                 me.getComponent("myFontStyleSetcolor_id").setValue(c5);
	                me.setColor(c5);
	                //默认初始化值
	                me.colorValue="color:"+c5+";";
	                 break;
	                case "text-decoration"://字体删除线/下划线
	                var td=temp2[1].replace("'","");
	                td=td.replace("'","");
	                if(td.indexOf("line-through underline")==0)//下划线
	                { 
	                Ext.fly("lblExample").setStyle("text-decoration",'');
	                me.textDecoration="text-decoration:line-through underline"//删除线/下划线
	                Ext.fly("lblExample").setStyle("text-decoration",'line-through underline');
	                 me.getComponent("mycboTextLineThrough_id").setValue(true);//下划线mycboTextDecoration_id
	                 me.getComponent("mycboTextDecoration_id").setValue(true);//删除线
	                }
	                else if(td.indexOf("underline")==0)//下划线
	                {
	                    me.textDecoration='';
	                    me.textDecoration="text-decoration:underline"//下划线
	                    Ext.fly("lblExample").setStyle("text-decoration",'underline');
	                     me.getComponent("mycboTextLineThrough_id").setValue(true);//下划线mycboTextDecoration_id
	                 }
	                 else if(td.indexOf("line-through")==0)//下划线
	                {
	                    me.textDecoration='';
	                    Ext.fly("lblExample").setStyle("text-decoration",'');
	                    me.textDecoration="text-decoration:line-through"//下划线
	                    Ext.fly("lblExample").setStyle("text-decoration",'line-through');
	                    me.getComponent("mycboTextDecoration_id").setValue(true);//删除线
	                 }else{
	                 me.textDecoration='';
	                 me.getComponent("mycboTextLineThrough_id").setValue(false);//下划线mycboTextDecoration_id
	                 me.getComponent("mycboTextDecoration_id").setValue(false);//删除线
	                 }
	                 break;
	                default:
	                    
	                }
	   }  
	   }else{
	        //默认初始化值
	        me.colorValue='color:#000000;';
	        me.getComponent("myFontStyleSetcolor_id").setValue("#000000");
	        me.fontSize="font-size:12px;";
	   }
    },
    /**
     * 获取设置当前控件的文字属性值
     * @return {}
     */
    GetValue:function(){
		var me=this;
	    var lastValue="";
	    if(me.fontStyle!=null||me.fontStyle!="")
	    {lastValue=lastValue+me.fontStyle}
	    if(me.fontSize!=null||me.fontSize!="")
	    {lastValue=lastValue+me.fontSize}
	    if(me.fontFamily!=null||me.fontFamily!="")
	    {lastValue=lastValue+me.fontFamily}
	    if(me.colorValue!=null||me.colorValue!="")
	    {lastValue=lastValue+me.colorValue}
	    if(me.textDecoration!=null||me.textDecoration!="")
	    {lastValue=lastValue+me.textDecoration}
	    lastValue=lastValue+"";
	    return me.font=lastValue;//
    },

    /**
     * 字体数据源
     * @return {}
     */
   getcbofontStore:function(){
	   var cbofontStore = Ext.create('Ext.data.Store', {
	   fields: ['displayField', 'valueField'],
	   autoLoad:true,
	   data:[   
	   {displayField:'新细明体',valueField:'PMingLiU'},
	        {displayField:'细明体',valueField:'MingLiU'},   
	        {displayField:'标楷体',valueField:'DFKai-SB'}, 
	        {displayField:'黑体',valueField:'SimHei'},   
	        {displayField:'宋体',valueField:'SimSun'},   
	        {displayField:'新宋体',valueField:'NSimSun'},   
	        {displayField:'仿宋',valueField:'FangSong'},   
	        {displayField:'楷体',valueField:'KaiTi'},   
	        {displayField:'仿宋_GB23122',valueField:'FangSong_GB2312'},
	        {displayField:'楷体_GB2312',valueField:'KaiTi_GB2312'},
	        {displayField:'微软正黑体',valueField:'Microsoft JhengHei'},   
	        {displayField:'微软雅黑',valueField:'Microsoft YaHei'},
	        {displayField:'隶书',valueField:'LiSu'},
	        //Office
	        {displayField:'幼圆',valueField:'YouYuan'},   
	        {displayField:'华文细黑',valueField:'STXihei'},   
	        {displayField:'华文楷体 ',valueField:'STKaiti'},
	        {displayField:'华文宋体',valueField:'STSong'},
	        {displayField:'华文中宋',valueField:'STZhongsong'},   
	        {displayField:'华文仿宋',valueField:'STFangsong'},
	        {displayField:'方正舒体',valueField:'FZShuTi'},
	        {displayField:'方正姚体',valueField:'FZYaoti'},
	        {displayField:'华文彩云',valueField:'STCaiyun'},
	        {displayField:'华文隶书',valueField:'STLiti'},
	        {displayField:'华文行楷',valueField:'STXingkai'},
	        {displayField:'华文新魏',valueField:'STXinwei'}
	    ]
	    });
	    return cbofontStore;
    },
    /**
     * 字形数据源
     * @return {}
     */
   getcbofontweightStore:function(){
	   var cbofontweightStore = Ext.create('Ext.data.Store', {
	   fields: ['displayField', 'valueField'],
	   data:[   
	        {displayField:'常规',valueField:'font-style:normal;'},//
	        {displayField:'斜体',valueField:'font-style:italic;'}, 
	        {displayField:'倾斜',valueField:'font-style:oblique;'}, 
	        {displayField:'加粗',valueField:'font-weight:bold;'},
	        {displayField:'加粗 倾斜',valueField:'font-weight:bold;font-style:oblique;'}
	    ]
	    });
	    return cbofontweightStore;
    },
   setFontSize:function(fontsize){
     Ext.fly("lblExample").setStyle("font-size",fontsize+"px");
    },
    /**
     * 创建字体大小选择器
     * @return {}
     */
   createNumber:function(){//大小(S)
    var me=this;
    var number=Ext.create("Ext.form.field.Number",{
        width :100,
        maxWidth :100,
        x:217,y:28,
        itemId:"myFontStyleSetfontSize_id",
        value: 12,
        maxValue: 72,
        minValue: 5,
        maxText:"只允许录入数字值范围为5~72",
        minValue:"只允许录入数字值范围为5~72",
        nanText:"输入值为非有效数值",
        negativeText:"不能录入0或者负数",
         listeners:{
                'change': function(numberfield1, newValue, oldValue, eOpts){
                 me.fontSize="font-size:"+"" +newValue+"px;";
                 me.setFontSize(newValue);
                }
           }
   }
   );
   return number;
     },
     /**
     * 创建颜色选择器
     * @return {}
     */
   createColor:function(){//大小(S)
    var me=this,colorValue;
   var color=Ext.create("Ext.form.field.Text",
   {
        width:100,
        x:8,y:150,
        value:'#000000',
        itemId:"myFontStyleSetcolor_id",
        blankText :'点击我',
        listeners:{
             'focus':function(dd,The, eOpts ){
                //创建和弹出创建颜色选择器
               if(!me.win){
                me.createWin();
               }
               else{
                 me.win.show();
               }
                },
                change:function(dd,newValue,oldValue,eOpts ){
                }
           }
   }
   );
   return color;
    },
    /**
     * 给测试用例赋值(颜色选择)
     * @param {} color
     */
    setColor:function(color2){
      Ext.fly("lblExample").setStyle("color",color2);
    },
	 /**
	  * 创建和弹出创建颜色选择器
	  */
    createWin:function(){
	  var me=this;
	  var xy=me.getPosition();
	        if (!me.win) {
                me.win = null;
	            me.win = Ext.create('widget.window', {
	                title: '颜色选择器',
	                closable: true,
	                closeAction: 'hide',
	                width: 200,
	                minWidth: 200,
	                height: 160,
	                x:xy[0]+38,y:xy[1]+200,
	                layout: {
	                    type: 'border',
	                    padding: 5
	                },
	                items: [
	                        Ext.create('Ext.ColorPalette', {
	                        listeners: {
	                          select: function(cp, color4){
	                          var c1="#"+color4;
	                          me.getComponent("myFontStyleSetcolor_id").setValue(c1);
	                          me.colorValue="color:"+c1+";"//'+"\'"+'
	                          //示例
	                          me.setColor(c1);
                              me.win.hide();
                              //this.hide();
	                                    }
	                                }
	                            })
	                         ]
	            });
	        }else{
	        me.win.show();
	        }
	         if (me.win.isVisible()) {
	            //me.win.hide();
	        } else {
	            me.win.show();
	        }
      },  
    initComponent:function(){
        var me = this;
        me.items = [
         {//第一行:字体
            xtype:'label',text:'字体(F)',
            cls:(me.labelcls.length>0)?(me.labelcls):{fontSize:'16px',color:'black',fontWeight:'bold'},//字体属性设置:label样式,
            x:8,y:5
        },
         {//第二行:字体下拉列表
			xtype:'combobox',width:100,height:48,//id: 'cbofont',
            queryMode: 'local',
            valueField:'valueField',
            displayField :'displayField',
            itemId:"myFontStyleSetfontFamily_id",
            store:me.getcbofontStore(),
			x:8,y:15,
            value:'SimSun',//默认值--宋体
            listeners:{
                'select': function(combo, records, eOpts ){
                    Ext.Array.each(records, function (model) {
                    //示例
                    me.fontFamily="font-family:"+model.get('valueField')+";";
                    Ext.fly("lblExample").setStyle("font-family",model.get('valueField'));
                    });
                }
           }
		},
          {//字形(字体风格)
			xtype:'label',text:'字形(Y)',
			cls:(me.labelcls.length>0)?(me.labelcls):{fontSize:'16px',color:'black',fontWeight:'bold'},//字体属性设置:label样式,
			x:113,y:5
		},
         {//第二行:字形(字体风格)下拉列表
            xtype:'combobox',width:100,height:48,//itemId: 'cbofontweight',
            queryMode: 'local',
            valueField:'valueField',
            displayField :'displayField',
            itemId:"myFontStyleSetfontStyle_id",
            store:me.getcbofontweightStore(),
            x:113,y:15,
            value:'font-style:normal;',//默认值--常规
                listeners:{
                'select': function(combo, records, eOpts ){
                    Ext.Array.each(records, function (model) {
                    //示例
                    me.fontStyle=model.get('valueField');
                    Ext.fly("lblExample").setStyle("font-style",model.get('valueField'));
                    var displayField2=model.get('displayField');
                switch(displayField2)
                {
                case "常规":
                Ext.fly("lblExample").setStyle("font-style","normal");
                    break;
                case "斜体":
              Ext.fly("lblExample").setStyle("font-style","italic");
                 break;
                case "倾斜":
              Ext.fly("lblExample").setStyle("font-style","oblique");
                 break;
                case "加粗":
                //me.fontWeight=
              Ext.fly("lblExample").setStyle("font-weight","bold");
                 break;
                case "加粗 倾斜":
              Ext.fly("lblExample").setStyle("font-weight","bold");
              Ext.fly("lblExample").setStyle("font-style","oblique");
                 break;
                default:
                }
                    });
                }
           }
        },
          {//大小(S)
			xtype:'label',text:'大小(S)',cls:(me.labelcls.length>0)?(me.labelcls):{fontSize:'16px',color:'black',fontWeight:'bold'},//字体属性设置:label样式,
			x:217,y:5
		},
       {//第二行:字体大小选择器
       xtype: me.createNumber()
        },
         {//第三行 :效果
            xtype:'label',text:'效果',
            cls:(me.labelcls.length>0)?(me.labelcls):{fontSize:'16px',color:'black',fontWeight:'bold'},//字体属性设置:label样式,
            x:8,y:60
        },
         {//第四行 :字体删除线
            xtype:'checkboxfield',
            boxLabel  : '删除线',
            name      : 'checkboxTypeface',
            inputValue: '1',
            itemId    : 'mycboTextLineThrough_id',
            x:28,y:80,
            listeners:{
               'change': function(combo, newValue,oldValue, eOpts ){
                me.textDecorationChecked();
                }
           }
        },
          {//第五行 字体下划线
           xtype:'checkboxfield',//itemId: 'textdecoration',
           boxLabel  : '下划线',
           name      : 'checkboxTypeface',
           inputValue: '2',
           itemId        : 'mycboTextDecoration_id',
           x:28,y:98,
           listeners:{
                'change': function(combo, newValue,oldValue, eOpts ){
                me.textDecorationChecked();
                }
           }
        },
          {//第六行:颜色(C)
            xtype:'label',text:'颜色(C)',cls:(me.labelcls.length>0)?(me.labelcls):{fontSize:'16px',color:'black',fontWeight:'bold'},//字体属性设置:label样式,
            x:8,y:125
        },
         {//第七行:颜色(C)createColor
            xtype:me.createColor()
        },
         {//第三到七行:示例
            xtype:'panel',width:290,height:110,id:'panelss',
            items:[
            {//颜色(C)
            xtype:'label',text:' 示例: 字体文字属性设置示例效果',id:"lblExample",
            style:(this.font.length>0)?('{'+this.font+'}'):({fontSize:'14px',color:'black',fontWeight:'bold'})    
        }
            ],
            x:140,y:90
        },
          //第二到七行
         {//确定
            xtype:'button',width:100,text:'确定',itemId:'myfontstylesetbtn_ok',
            x:330,y:25,
            hidden:me.btnHidden,
            listeners:{
                'click': function(){
                    //long7
                me.GetValue();
                me.fireEvent('onOKCilck');
                if ((me.win!=null)||(me.win!=null&&me.win.isVisible())) {
		          me.win.hide();
		        }
                }
           }
        },
          //第二到七行
         {//取消
            xtype:'button',width:100, text:'取消',itemId:'myfontstylesetbtn_Cancel',
            hidden:me.btnHidden,
            x:330,y:60,
            listeners:{
                'click': function(){
                me.fireEvent('onCancelCilck');
                me.fireEvent('onOKCilck');
                 if ((me.win!=null)||(me.win!=null&&me.win.isVisible())) {
                  me.win.hide();
                }
                }
            }
        }
        ];
		//添加事件，别的地方就能对这个事件进行监听
        this.addEvents('onOKCilck');//确认按钮
        this.addEvents('onCancelCilck');//取消按钮
    	this.callParent(arguments);
    },
    
    /**
     * 删除线/下划线处理
     */
    textDecorationChecked:function() {
    var me = this;
    //取itemId
    var c2=me.getComponent("mycboTextLineThrough_id").checked;//下划线mycboTextDecoration_id
    var c1=me.getComponent("mycboTextDecoration_id").checked;//删除线
    
    if(c1&&c2){
        Ext.fly("lblExample").setStyle("text-decoration",'');
        me.textDecoration="text-decoration:line-through underline"//删除线/下划线
        Ext.fly("lblExample").setStyle("text-decoration",'line-through underline');
            }
     else if(c1){
        me.textDecoration='';
        me.textDecoration="text-decoration:underline"//下划线
        Ext.fly("lblExample").setStyle("text-decoration",'underline');
            }
      else if(c2){
        Ext.fly("lblExample").setStyle("text-decoration",'');
        me.textDecoration="text-decoration:line-through"//删除线
        Ext.fly("lblExample").setStyle("text-decoration",'line-through');
           }else{
            Ext.fly("lblExample").setStyle("text-decoration",'');
            this.textDecoration='';
           }
    },
    afterRender: function() {
        var me = this;
        
        me.callParent(arguments);
    },
    destroy:function(){
     var me = this;
     if ((me.win!=null)||(me.win!=null&&me.win.isVisible())) {
         me.win.hide();
       }
    }
});