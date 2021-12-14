/**
 * 颜色下拉选择器
 * 【必配参数】
 * 
 * 【可选参数】
 * valueField 值字段，默认value
 * displayField 显示文字字段，默认text
 * minWidth 下拉列表框的最小宽度，默认是combo组件的宽度
 * maxHeight 下拉列表框的最大高度，默认是combo组件的高度
 * ignoreCase 是否忽略大小写，默认true
 * forceSelection 是否所选的值必须存在于列表中，默认true
 * autoSelectNum 默认选中第几条，默认0
 * 
 * Example:
 * var com2 = Ext.create('Ext.zhifangux.colorscombobox',{
 *		valueField:'value',//值
 *		displayField:'text',//显示文字
 *		minWidth:140,
 *		maxHeight:200,
 *		queryFields:['value','text'],
 *		forceSelection:false//true:所选的值必须存在于列表中;false:允许设置任意文本
 *	});
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.colorscombobox',{
	extend:'Ext.form.field.ComboBox',
	alias:['widget.colorscombobox','widget.colorscombo'],
	//=====================可配参数=======================
	/**
	 * 值字段
	 * @type String
	 */
	valueField:'value',
	/**
	 * 显示文字字段
	 * @type String
	 */
	displayField:'text',
    /**
     * 是否所选的值必须存在于列表中
     * true:所选的值必须存在于列表中;
     * false:允许设置任意文本
     * @type Boolean
     */
    forceSelection:true,
    /**
     * 是否忽略大小写
     */
    ignoreCase:true,
    /**
     * 默认选中第几条
     * @type Number
     */
    autoSelectNum:0,
    name:'',
    gridWidth:0,
    gridHeight:0,
    editable:true,//可修改
    queryMode:'local',
    isValueColumns:true,//是否隐藏颜色列,隐藏:true,显示:false
    queryFields:['value','text'],
    minWidth:140,
    maxHeight:200,
    store:new Ext.data.Store({
            fields:['value','text'],
            data:[
                {value:'LightPink',text:'浅粉红'},
                {value:'Pink',text:'粉红色'},
                {value:'LightPink',text:'猩红色'},
                {value:'LavenderBlush',text:'淡紫色'},
                {value:'PaleVioletRed',text:'紫罗兰红'},
                {value:'HotPink',text:'热情粉红'},
                {value:'DeepPink',text:'深粉色'},
                {value:'MediumVioletRed',text:'兰花紫'},
                {value:'plum',text:'李子色'},
                {value:'Violet',text:'紫罗兰'},
                
                {value:'Magenta',text:'洋红色'},
                {value:'Fuchsia',text:'紫红色'},
                {value:'DarkMagenta',text:'深洋红色'},
                {value:'Purple',text:'紫色'},
                {value:'MediumOrchid',text:'兰花紫'},
                {value:'DarkViolet',text:'深紫罗兰'},
                {value:'DarkOrchid',text:'深兰花紫'},
                {value:'Indigo',text:'靛青色'},
                {value:'BlueViolet',text:'深紫罗兰蓝色'},
                {value:'MediumPurple',text:'中度紫色'},
                
                {value:'MediumSlateBlue',text:'中度暗蓝灰'},
                {value:'SlateBlue',text:'板岩暗蓝灰'},
                {value:'DarkSlateBlue',text:'深岩暗蓝灰'},
                {value:'Lavender',text:'熏衣草花淡紫'},
                {value:'GhostWhite',text:'幽白色'},
                {value:'Blue',text:'蓝色'},
                {value:'MediumBlue',text:'适中蓝色'},
                {value:'MidnightBlue',text:'午夜蓝色'},
                {value:'DarkBlue',text:'深蓝色'},
                {value:'Navy',text:'海军蓝'},
                
                {value:'RoyalBlue',text:'皇军蓝色'},
                {value:'CornflowerBlue',text:'矢车菊蓝'},
                {value:'LightSteelBlue',text:'淡钢蓝'},
                {value:'LightSlateGray',text:'浅石板灰'},
                {value:'SlateGray',text:'石板灰色'},
                {value:'DodgerBlue',text:'道奇蓝'},
                {value:'AliceBlue',text:'爱丽丝蓝'},
                {value:'SteelBlue',text:'钢蓝色'},
                {value:'LightSkyBlue',text:'淡天蓝色'},
                {value:'SkyBlue',text:'天蓝色'},
                
                {value:'DeepSkyBlue',text:'深天蓝'},
                {value:'LightBLue',text:'淡蓝'},
                {value:'PowDerBlue',text:'火药蓝'},
                {value:'CadetBlue',text:'军校蓝'},
                {value:'Azure',text:'蔚蓝色'},
                {value:'LightCyan',text:'淡青色'},
                {value:'PaleTurquoise',text:'苍白绿宝石'},
                {value:'Cyan',text:'青色'},
                {value:'Aqua',text:'水绿色'},
                {value:'DarkTurquoise',text:'深绿宝石'},
                
                {value:'DarkSlateGray',text:'深石板灰'},
                {value:'DarkSlateGray',text:'深青色'},
                {value:'Teal',text:'水鸭色'},
                {value:'MediumTurquoise',text:'适中绿宝石'},
                {value:'LightSeaGreen',text:'浅海洋绿'},
                {value:'Turquoise',text:'绿宝石'},
                {value:'Aquamarine',text:'碧绿色'},
                {value:'MediumAquamarine',text:'适中碧绿色'},
                {value:'MediumSpringGreen',text:'适中春绿色'},
                {value:'MintCream',text:'薄荷奶油'},
                
                {value:'SpringGreen',text:'春绿色'},
                {value:'MediumSeaGreen',text:'适中海洋绿'},
                {value:'SeaGreen',text:'海洋绿'},
                {value:'Honeydew',text:'蜂蜜'},
                {value:'LightGreen',text:'淡绿色'},
                {value:'PaleGreen',text:'苍白绿色'},
                {value:'DarkSeaGreen',text:'深海洋绿'},
                {value:'LimeGreen',text:'酸橙绿'},
                {value:'Lime',text:'酸橙色'},
                {value:'ForestGreen',text:'森林绿'},
                
                {value:'Green',text:'绿色'},
                {value:'DarkGreen',text:'深绿色'},
                {value:'Chartreuse',text:'查特酒绿'},
                {value:'LawnGreen',text:'草坪绿'},
                {value:'GreenYellow',text:'绿黄色'},
                {value:'DarkOliveGreen',text:'深橄榄绿'},
                {value:'YellowGreen',text:'黄绿色'},
                {value:'OliveDrab',text:'橄榄土褐色'},
                {value:'Beige',text:'米色(浅褐色)'},
                {value:'LightGoldenrodYellow',text:'浅秋麒麟黄'},
                
                {value:'Ivory',text:'象牙色'},
                {value:'LightYellow',text:'浅黄色'},
                {value:'Yellow',text:'黄色'},
                {value:'Olive',text:'橄榄'},
                {value:'DarkKhaki',text:'深卡其布'},
                {value:'LemonChiffon',text:'柠檬薄纱'},
                {value:'PaleGoldenrod',text:'灰秋麒麟'},
                {value:'Khaki',text:'卡其布'},
                {value:'Gold',text:'金黄色'},
                {value:'Cornsilk',text:'玉米色'},
                
                {value:'GoldEnrod',text:'秋麒麟'},
                {value:'DarkGoldEnrod',text:'深秋麒麟'},
                {value:'FloralWhite',text:'花白色'},
                {value:'FloralWhite',text:'老饰带'},
                {value:'Wheat',text:'小麦色'},
                {value:'Moccasin',text:'鹿皮鞋'},
                {value:'Orange',text:'橙色'},
                {value:'PapayaWhip',text:'番木瓜'},
                {value:'BlanchedAlmond',text:'漂白杏仁'},
                {value:'NavajoWhite',text:'Navajo白'},
                
                {value:'AntiqueWhite',text:'古白色'},
                {value:'Tan',text:'晒黑色'},
                {value:'BurlyWood',text:'结实树'},
                {value:'Bisque',text:'乳脂/番茄'},
                {value:'DarkOrange',text:'深橙色'},
                {value:'Linen',text:'亚麻布'},
                {value:'Peru',text:'秘鲁色'},
                {value:'PeachPuff',text:'桃色'},
                {value:'SandyBrown',text:'沙棕色'},
                {value:'Chocolate',text:'巧克力'},
                
                {value:'SaddleBrown',text:'马鞍棕色'},
                {value:'SeaShell',text:'海贝壳'},
                {value:'Sienna',text:'黄土赭色'},
                {value:'LightSalmon',text:'浅鲜肉(鲑鱼)色'},
                {value:'Coral',text:'珊瑚色'},
                {value:'OrangeRed',text:'橙红色'},
                {value:'DarkSalmon',text:'深鲜肉(鲑鱼)色'},
                {value:'Tomato',text:'番茄色'},
                {value:'MistyRose',text:'薄雾玫瑰'},
                {value:'Salmon',text:'鲜肉(鲑鱼)色'},
                
                {value:'Snow',text:'雪色'},
                {value:'LightCoral',text:'淡珊瑚色'},
                {value:'RosyBrown',text:'玫瑰棕色'},
                {value:'IndianRed',text:'印度红'},
                {value:'Red',text:'红色'},
                {value:'Brown',text:'棕色'},
                {value:'FireBrick',text:'耐火砖'},
                {value:'DarkRed',text:'深红色'},
                {value:'Maroon',text:'栗色'},
                {value:'White',text:'白色'},
                
                {value:'WhiteSmoke',text:'白烟色'},
                {value:'Gainsboro',text:'Gainsboro'},
                {value:'LightGrey',text:'浅灰色'},
                {value:'Silver',text:'银白色'},
                {value:'DarkGray',text:'深灰色'},
                {value:'Gray',text:'灰色'},
                {value:'DimGray',text:'暗淡灰色'},
                {value:'Black',text:'黑色'}
                
            ]
        }),
    /***
     * 创建数据源
     * @return {}
     */
    createColumns:function(){
        var me=this;
        var value=me.valueField;
        var text=me.displayField;
        var columns=[];
        var valueColumns={text:'颜色值',dataIndex:value,width:80,align: 'left',hidden:me.isValueColumns,
                renderer:function(value, cellmeta, record, rowIndex, columnIndex, store){
                    var colorValue=record.get(me.valueField);
                    if(colorValue==''){
                        colorValue='#FFFFFF';//默认为白色
                    }else{
                        colorValue=record.get(me.valueField);
                    }
                    cellmeta.style='background-color:'+colorValue;//设置背景色#0000ff
                    return value;
                }};
        var textColumns={text:'颜色名称',dataIndex:text,width:100,align: 'left',
                renderer:function(value, cellmeta, record, rowIndex, columnIndex, store){
                    var colorValue=record.get(me.valueField);
                    if(colorValue==''){
                        colorValue='#FFFFFF';//默认为白色
                    }else{
                        colorValue=record.get(me.valueField);
                    }
                    cellmeta.style='background-color:'+colorValue;//设置背景色#0000ff
                    return value;
                }};
          columns.push(valueColumns);
          columns.push(textColumns);
        return columns;
    },
                    
	/**
	 * 初始化属性
	 * @private
	 */
	initComponent:function(){
		var me = this;
        me.columns=me.createColumns();//创建列
        //me.stroe=me.createStroe();
		me.listConfig = me.createListConfig();//创建配置属性
		me.callParent(arguments);
	},
	/**
	 * 创建配置属性
	 * @private
	 * @return {}
	 */
	createListConfig:function(){
		var me = this;
		var com = {
			xtype:'grid',
			resizable:true,
			columns:me.columns ? me.columns:[],
			minWidth:me.minWidth ? me.minWidth:me.width,
			maxWidth:me.maxWidth ? me.maxWidth:me.width,
			maxHeight:me.maxHeight ? me.maxHeight:me.height,
			emptyText:me.emptyText ? me.emptyText:'',
			viewConfig:{
		        emptyText:'没有数据！',
		        loadingText:'获取数据中，请等待...'
			},
			listeners:{
				resize:function(com,width,height,oldWidth,oldHeight,eOpts){//列表大小变化
					if(width != me.minWidth){
						me.gridWidth = width;
					}
					if(height != me.height){
						me.gridHeight = height;;
					}
				}
			}
		}
		return com;
	},
	/**
	 * 根据index选中一条数据
	 * @private
	 * @param {} index
	 */
	selectRecordByIndex:function(index){
		var me = this;
		var picker = me.getPicker();
		if(typeof index == "number" && index > -1){
			picker.getSelectionModel().select(index);
		}
	},
	/**
	 * 展开处理
	 * @private
	 */
	onExpand:function(){
        var me = this,
            keyNav = me.listKeyNav,
            selectOnTab = me.selectOnTab,
            picker = me.getPicker().view;
            
        if (keyNav) {
        	keyNav.boundList.ownerCt.setWidth(me.gridWidth);
        	keyNav.boundList.ownerCt.setHeight(me.gridHeight);
            keyNav.enable();
        } else {
        	var index = me.store.find(me.valueField,me.value);
        	if(index != -1){
        		me.getPicker().getSelectionModel().select(index);
        	}
        	
            keyNav = me.listKeyNav = new Ext.view.BoundListKeyNav(this.inputEl, {
                boundList: picker,
                forceKeyDown: true,
                tab: function(e) {
                    if (selectOnTab) {
                        this.selectHighlighted(e);
                        me.triggerBlur();
                    }
                    return true;
                },
                up: function(e) {
		            var com = me.getPicker();
		            var allItems = com.view.all;
		            var oldItemIdx = me.getItemIndexByItem(com.getSelectionModel().getLastSelected());
		            newItemIdx = oldItemIdx > 0 ? oldItemIdx - 1 : allItems.getCount() - 1;
		            me.selectRecordByIndex(newItemIdx);
		        },
		        down: function() {
		            var com = me.getPicker();
		            var allItems = com.view.all;
		            var oldItemIdx = me.getItemIndexByItem(com.getSelectionModel().getLastSelected());
		            newItemIdx = oldItemIdx < allItems.getCount() - 1 ? oldItemIdx + 1 : 0;
		            me.selectRecordByIndex(newItemIdx);
		        },
                pageUp: function() {
		             var com = me.getPicker(),
		             	rowsVisible = me.getRowsVisible(),
			            selIdx,
			            prevIdx,
			            prevRecord;
			
			        if (rowsVisible) {
		            	selIdx = me.getItemIndexByItem(com.getSelectionModel().getLastSelected());
			            prevIdx = selIdx - rowsVisible;
			            if (prevIdx < 0) {
			                prevIdx = 0;
			            }
			            me.selectRecordByIndex(prevIdx);
			        }
		        },
		        pageDown:function(){
		        	var com = me.getPicker(),
		        		rowsVisible = me.getRowsVisible(),
			            selIdx,
			            nextIdx,
			            nextRecord;
			
			        if (rowsVisible) {
			            selIdx = me.getItemIndexByItem(com.getSelectionModel().getLastSelected());
			            nextIdx = selIdx + rowsVisible;
			            if (nextIdx >= com.store.getCount()) {
			                nextIdx = com.store.getCount() - 1;
			            }
			            me.selectRecordByIndex(nextIdx);
			        }
		        },
            	home:function(){
            		var com = me.getPicker();
            		me.selectRecordByIndex(0);
            	},
            	end:function(){
            		var com = me.getPicker();
			        me.selectRecordByIndex(me.store.getCount() - 1);
            	},
            	enter:function(){
            		me.doEnter();
            	}
            });
			//列表中回车键选中数据
			Ext.create('Ext.util.KeyNav', picker.el, {
				enter:function(e){  
			        me.doEnter();
			    },  
			    scope:this  
			});
        }
        if (selectOnTab) {
            me.ignoreMonitorTab = true;
        }
        Ext.defer(keyNav.enable, 1, keyNav);
        me.inputEl.focus();
	},
	/**
	 * 检索
	 * @param {} queryString
	 * @param {} forceAll
	 * @param {} rawQuery
	 * @return {Boolean}
	 */
	doQuery:function(queryString,forceAll,rawQuery){
        queryString = queryString || '';
        var me = this,
            qe = {
                query: queryString,
                forceAll: forceAll,
                combo: me,
                cancel: false
            },
            store = me.store,
            isLocalMode = me.queryMode === 'local',
            needsRefresh;
        if (me.fireEvent('beforequery', qe) === false || qe.cancel) {
            return false;
        }
        queryString = qe.query;
        forceAll = qe.forceAll;
        
        me.justQuery = true;
        
        if (forceAll || (queryString.length >= me.minChars)) {
        	me.store.clearFilter();
            me.expand();
            me.lastQuery = queryString;
            if (isLocalMode) {
                store.suspendEvents();
                needsRefresh = me.clearFilter();
                if (queryString || !forceAll) {
                    me.activeFilter = new Ext.util.Filter({
                    	filterFn: function(item){
                    		//检索的逻辑
                    		return me.queryLogic(item,queryString);
					    }
                    });
                    
                    store.filter(me.activeFilter);
                    needsRefresh = true;
                } else {
                    delete me.activeFilter;
                }
                store.resumeEvents();
                if (me.rendered && needsRefresh) {
                	me.getPicker().view.refresh();
                }
            } else {
                me.rawQuery = rawQuery;
                if (me.pageSize) {
                    me.loadPage(1);
                } else {
                    store.load({
                        params: me.getParams(queryString)
                    });
                }
            }

            if (isLocalMode) {
                me.doAutoSelect();
            }
            if (me.typeAhead) {
                me.doTypeAhead();
            }
        }
        return true;
    },
    assertValue: function() {
        var me = this,
            value = me.getRawValue(),
            rec;
        if (me.multiSelect) {
            if (value !== me.getDisplayValue()) {
                me.setValue(me.lastSelection);
            }
        } else {
            rec = me.findRecordByDisplay(value);
            if (rec) {
                me.select(rec);
            } else {
                //me.setValue(me.lastSelection);
            }
        }
        me.collapse();
    },
    /**
     * 输入框失去焦点前处理
     * @private
     */
    beforeBlur: function() {
        this.doQueryTask.cancel();
        this.assertValue();
    	var me = this;
    	var value = me.getValue();
    	var rawValue = me.getRawValue();
    	
    	var items = me.store.data.items;
    	var count = 0;
    	for(var i in items){
			if(items[i].get(me.valueField) === value && items[i].get(me.displayField) === rawValue){
				count++;
			}
		}
		
    	if((count != 1 || me.justQuery) && me.forceSelection){
    		me.setValue("");
    		me.setRawValue("");
    	}
    	me.store.clearFilter();
    },
    /**
     * 表格模板
     * @private
     * @return {}
     */
    createPicker:function() {
        var me = this,
            picker,
            pickerCfg = Ext.apply({
                xtype: 'boundlist',
                pickerField: me,
                selModel: {
                    mode: me.multiSelect ? 'SIMPLE' : 'SINGLE'
                },
                floating: true,
                hidden: true,
                store: me.store,
                displayField: me.displayField,
                focusOnToFront: false,
                pageSize: me.pageSize,
                tpl: me.tpl
            }, me.listConfig, me.defaultListConfig);

        picker = me.picker = Ext.widget(pickerCfg);
        if (me.pageSize) {
            picker.pagingToolbar.on('beforechange', me.onPageChange, me);
        }
        me.mon(picker, {
            itemdblclick: me.onItemdblClick,
            refresh: me.onListRefresh,
            scope: me
        });
        return picker;
    },
     /**
     * 双击选中
     * @private
     * @param {} picker
     * @param {} record
     */
    onItemdblClick:function(picker, record){
    	var me = this;
    	me.justQuery = false;
    	me.onListSelectionChange(picker, record);
    	me.onItemClick(picker, record);
    	me.beforeBlur();
    },
    /**
     * 回车键处理
     * @private
     */
    doEnter:function(){
    	var me = this;
    	me.justQuery = false;
    	var picker = me.getPicker().view;
    	var records = picker.getSelectionModel().getSelection();
    	me.onListSelectionChange(picker,records);
    },
    /**
     * 快捷键上下翻页处理
     * 根据下拉框所见高度返回需要跳跃的条数
     * @private
     * @return {}
     */
    getRowsVisible: function() {
        var me = this,
        	rowsVisible = false,
            view = me.getPicker().view,
            row = view.getNode(0),
            rowHeight, gridViewHeight;
        if (row) {
            rowHeight = Ext.fly(row).getHeight();
            gridViewHeight = view.el.getHeight();
            rowsVisible = Math.floor(gridViewHeight / rowHeight);
        }
        return rowsVisible;
    },
    /**
     * 默认选中的行
     * @private
     */
    doAutoSelect: function() {
        var me = this;
        var picker = me.getPicker();
        if (picker && me.autoSelect && me.store.getCount() > 0) {
        	me.getPicker().show();//显示下拉列表
        	var lastSelected = picker.getSelectionModel().lastSelected;
        	var value = me.getRawValue();
        	if(!value || value == ""){
        		lastSelected = null;
        	}
        	picker.getSelectionModel().deselectAll();
        	setTimeout(function(){picker.getSelectionModel().select(lastSelected || 0);},50);
        	picker.getSelectionModel().select(lastSelected || 0);
        }else{
        	me.getPicker().hide();//隐藏下拉列表
        }
    },
    /**
     * 检索的逻辑
     * @private
     * @param {} item
   	 * @param {} queryString
     * @return {Boolean}
     */
    queryLogic:function(item,queryString){
    	var me = this;
    	if(queryString == ""){
			return true;
		}else{
			for(var i in me.queryFields){
    			var v = item.get(me.queryFields[i]) + "";
    			var str;
    			if (me.ignoreCase) {
    				v = v.toUpperCase();
    				str = queryString.toUpperCase(); 
    			}
    			if(v.indexOf(str) != -1){
    				return true;
    			}
    		}
		}
        return false;
    },
    /**
     * 根据一行数据获对象item取本条数据在当前过滤列表中的行号
     * @private
     * @param {} item
     * @return {}
     */
    getItemIndexByItem:function(item){
    	var me = this;
    	var items = me.getPicker().store.data.items;
        for(var i=0;i<items.length;i++){
        	if(item['data'][me.valueField] === items[i]['data'][me.valueField]){
        		return i;
        	}
        }
        return -1;
    },
    onKeyUp: function(e, t) {
        var me = this,
            key = e.getKey();

        if (!me.readOnly && !me.disabled && me.editable) {
            me.lastKey = key;
            if (!e.isSpecialKey() || key == e.BACKSPACE || key == e.DELETE) {
                me.doQueryTask.delay(me.queryDelay);
            }else{
	        	var lastSelected = me.getPicker().getSelectionModel().getLastSelected();
	        	setTimeout(function(){me.getPicker().getSelectionModel().select(lastSelected);},50);
	        }
        }

        if (me.enableKeyEvents) {
            me.callParent(arguments);
        }
    },
    /**
     * 下拉框展开按钮点击
     * @private
     */
    onTriggerClick: function() {
        var me = this;
        if (!me.readOnly && !me.disabled) {
            if (me.isExpanded) {
                me.collapse();
            } else {
                me.onFocus({});
                me.expand();
                me.doAutoSelect();
            }
            me.inputEl.focus();
        }
        me.justQuery = false;
    },
    /**
     * 加载处理
     * @private
     */
    onLoad:function() {
        var me = this,
            value = me.value;

        if (me.ignoreSelection > 0) {
            --me.ignoreSelection;
        }
        if (me.rawQuery) {
            me.rawQuery = false;
            me.syncSelection();
            if (me.picker && !me.picker.getSelectionModel().hasSelection()) {
                //me.doAutoSelect();
            }
        }
        else {
            if (me.value || me.value === 0) {
                me.setValue(me.value);
            } else {
                if (me.store.getCount()) {
                    //me.doAutoSelect();
                } else {
                    me.setValue(me.value);
                }
            }
        }
    }
});