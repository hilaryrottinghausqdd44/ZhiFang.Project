/**
 * @class Ext.ux.LiveSearchGridPanel
 * @extends Ext.grid.Panel
 * <p>A GridPanel class with live search support.</p>
 * @author Nicolas Ferrero
 */
Ext.define('Ext.ux.LiveSearchGridPanel', {
    extend: 'Ext.grid.Panel',
    requires: [
        'Ext.toolbar.TextItem',
        'Ext.form.field.Checkbox',
        'Ext.form.field.Text',
        'Ext.ux.statusbar.StatusBar'
    ],
    
    /**
     * @private
     * search value initialization
     */
    searchValue: null,
    
    /**
     * @private
     * The row indexes where matching strings are found. (used by previous and next buttons)
     */
    indexes: [],
    
    /**
     * @private
     * The row index of the first search, it could change if next or previous buttons are used.
     */
    currentIndex: null,
    
    /**
     * @private
     * 生成用于搜索的正则表达式.
     */
    searchRegExp: null,
    
    /**
     * @private
     * 区分大小写模式.
     */
    caseSensitive: false,
    
    /**
     * @private
     * 正则表达式模式.
     */
    regExpMode: false,
    /**
     * @private
     * 是否显示搜索工具栏label
     */
    isShowSearchFieldlabel:true,
    /**
     * @private
     * 是否显示搜索工具栏
     */
    isShowTbar:true,
    /**
     * @private
     * 是否显示前一行
     */
    isShowPrevious:false,
    /**
     * @private
     * 是否显示下一行
     */
    isShowNext:false,
    /**
     * @private
     * 是否显示启用正则表达式
     */
    isShowRegExpToggle:true,
    /**
     * @private
     * 是否显示区分大小写
     */
    isShowCaseSensitiveToggle:true,
    /**
     * @private
     * 是否显示搜索结果状态栏
     */
    isShowStatusBar:true,
    /***
     * 搜索工具栏停放位置
     * @type String
     */
    topOrbottom:'top', 
    /***
     * 搜索匹配行的选中样式
     * @type String
     */
    setMyRowClass:false,
    /***
     * 搜索文本输入框宽度
     * @type String
     */
    searchFieldWidth:150,
    /**
     * @cfg {String} matchCls
     * The matched string css classe.
     */
    matchCls: 'x-livesearch-match',
    
    defaultStatusText: '未找到匹配行',
    
    // Component initialization override: adds the top and bottom toolbars and setup headers renderer.
    /***
     * 将顶部和底部的工具栏渲染
     */
    initComponent: function() {
        var me = this;
        me.bbar = [];//底部工具栏
        me.tbar =[];//顶部工具栏
        me.viewConfig={
                getRowClass:function(record,index,rowParams,store){
                    return (me.setMyRowClass==true)?("row-s"):("");
                }
            };
        if(me.isShowTbar==true){
            if(me.topOrbottom=='top'){
                //在顶部工具栏创建检索开始部分
                me.tbar.push(me.createsearchFieldlabel(me.isShowSearchFieldlabel));
                me.tbar.push(me.createsearchField(me.isShowTbar));
                me.tbar.push(me.createonPrevious(me.isShowPrevious));
                me.tbar.push(me.createonNext(me.isShowNext));
                me.tbar.push('-');
                //在顶部工具栏创建启用正则表达式
                me.tbar.push(me.createRegExpToggle(me.isShowRegExpToggle));
                me.tbar.push(me.createRegExpTogglelabel(me.isShowRegExpToggle));
                //在顶部工具栏创建启用正则表达式
                me.tbar.push(me.createCaseSensitiveToggle(me.isShowCaseSensitiveToggle));
                me.tbar.push(me.createCaseSensitiveTogglelabel(me.isShowCaseSensitiveToggle));
                
            }else{
                //在底部工具栏创建检索开始部分
                me.bbar.push(me.createsearchFieldlabel(me.isShowSearchFieldlabel));
                me.bbar.push(me.createsearchField(me.isShowTbar));
                me.bbar.push(me.createonPrevious(me.isShowPrevious));
                me.bbar.push(me.createonNext(me.isShowNext));
                me.bbar.push('-');
                //在底部工具栏创建启用正则表达式
                me.bbar.push(me.createRegExpToggle(me.isShowRegExpToggle));
                me.bbar.push(me.createRegExpTogglelabel(me.isShowRegExpToggle));
                //在底部工具栏创建启用区分大小写
                me.bbar.push(me.createCaseSensitiveToggle(me.isShowCaseSensitiveToggle));
                me.bbar.push(me.createCaseSensitiveTogglelabel(me.isShowCaseSensitiveToggle));
                tbar='';
            }
           //在底部工具栏创建状态提示栏
            //me.bbar.push('-');
            me.bbar.push(me.createsearchStatusBar(me.isShowStatusBar));
            //me.bbar.push('-');
        }else{
	        me.bbar='';
	        me.tbar ='';
        }
        me.callParent(arguments);
    },
    /***
     * 创建检索
     */
    createsearchFieldlabel:function(isShow){
        var me=this;
        
        var com={
            xtype: 'label',
            hidden :(isShow==true)?(false):(true),
            text: '检索',
            margin: '0 0 0 4px'
           };
        return com;
    },
    /***
     * 创建检索
     */
    createsearchField:function(isShow){
        var me=this;
        var com={
             xtype: 'textfield',
             hidden :(isShow==true)?(false):(true),
             name: 'searchField',
             hideLabel: true,
             width: me.searchFieldWidth,
             listeners: {
                 change: {
                     //fn: me.onTextFieldChange,
                     fn: me.onSearch,
                     scope: this,
                     buffer: 300
                 }
             }
         };
        return com;
    },
    /***
     * 创建检索前一行
     */
    createonPrevious:function(isShow){
        var me=this;
        var com={
            xtype: 'button',
            hidden :(isShow==true)?(false):(true),
            text: '&lt;',
            tooltip: '检索前一行',
            handler: me.onPreviousClick,
            scope: me
                };
        return com;
    },
    /***
     * 检索下一行
     */
    createonNext:function(isShow){
        var me=this;
        var com={
            xtype: 'button',
            hidden :(isShow==true)?(false):(true),
            text: '&gt;',
            tooltip: '检索下一行',
            handler: me.onNextClick,
            scope: me
                };
        return com;
    },    

    /***
     * 创建启用正则表达式
     */
    createsearchStatusBar:function(isShow){
        var me=this;
        var com=Ext.create('Ext.ux.StatusBar', {
            defaultText: me.defaultStatusText,
            hidden :(isShow==true)?(false):(true),
            name: 'searchStatusBar'
        });
        return com;
    }, 
    /***
     * 创建启用正则表达式
     */
    createRegExpToggle:function(isShow){
        var me=this;
        var com={
            xtype: 'checkbox',
            hideLabel: true,
            hidden :(isShow==true)?(false):(true),
            margin: '0 0 0 4px',
            handler: me.regExpToggle,
            scope: me                
            };
        return com;
    },
    /***
     * 创建启用正则表达式
     */
    createRegExpTogglelabel:function(isShow){
        var me=this;
        var com={
            xtype: 'label',
            hidden :(isShow==true)?(false):(true),
            text: '启用正则',
            margin: '0 0 0 4px'
           };
        return com;
    }, 
   /***
     * 创建区分大小写
     */
    createCaseSensitiveToggle:function(isShow){
        var me=this;
        var com={
            xtype: 'checkbox',
            hidden :(isShow==true)?(false):(true),
            hideLabel: true,
            margin: '0 0 0 4px',
            handler: me.caseSensitiveToggle,
            scope: me
            };
        return com;
    },
    /***
     * 创建区分大小写
     */
    createCaseSensitiveTogglelabel:function(isShow){
        var me=this;
        var com=
            {
	          xtype: 'label',
	          hidden :(isShow==true)?(false):(true),
	          text: '区分大小写',
	          margin: '0 0 0 4px'
          }
        return com;
    }, 
    // afterRender override: it adds textfield and statusbar reference and start monitoring keydown events in textfield input 
    afterRender: function() {
        var me = this;
        me.callParent(arguments);
        me.textField = me.down('textfield[name=searchField]');
        me.statusBar = me.down('statusbar[name=searchStatusBar]');
    },
    // detects html tag
    tagsRe: /<[^>]*>/gm,
    
    // DEL ASCII code
    tagsProtect: '\x0f',
    
    // 检测正则表达式保留字
    regExpProtect: /\\|\/|\+|\\|\.|\[|\]|\{|\}|\?|\$|\*|\^|\|/gm,
    
    /***
     *在正常模式下，它返回值与保护的正则表达式字符。
     *在正则表达式模式，它返回的原始值，如果设置了RegExp是无效的除外。
     * @返回{字符串}值来处理文本框的值或空，如果是空白的或无效。
     *@私人
     */
    getSearchValue: function() {
        var me = this,
            value = me.textField.getValue();
            
        if (value === '') {
            me.viewConfig={
                getRowClass:function(record,index,rowParams,store){
                    return "";
                }
            };
            return null;
        }
        if (!me.regExpMode) {
            value = value.replace(me.regExpProtect, function(m) {
                return '\\' + m;
            });
        } else {
            try {
                new RegExp(value);
            } catch (error) {
                me.statusBar.setStatus({
                    text: error.message,
                    iconCls: 'x-status-error'
                });
                 me.viewConfig={
                getRowClass:function(record,index,rowParams,store){
                    return "";
                }
            };
                return null;
            }
            // this is stupid
            if (value === '^' || value === '$') {
                 me.viewConfig={
                getRowClass:function(record,index,rowParams,store){
                    return "";
                }
            };
                return null;
            }
        }

        return value;
    },
    
    /**
     * 在每个单元格查找所有字符串匹配搜索值
     * @private
     */
     onTextFieldChange: function() {
         var me = this,
             count = 0;

         me.view.refresh();
         // reset the statusbar
         me.statusBar.setStatus({
             text: me.defaultStatusText,
             iconCls: ''
         });

         me.searchValue = me.getSearchValue();
         me.indexes = [];
         me.currentIndex = null;

         if (me.searchValue !== null) {
             me.searchRegExp = new RegExp(me.searchValue, 'g' + (me.caseSensitive ? '' : 'i'));
             
             
             me.store.each(function(record, idx) {
                 var td = Ext.fly(me.view.getNode(idx)).down('td'),
                     cell, matches, cellHTML;
                 while(td) {
                     cell = td.down('.x-grid-cell-inner');
                     matches = cell.dom.innerHTML.match(me.tagsRe);
                     cellHTML = cell.dom.innerHTML.replace(me.tagsRe, me.tagsProtect);
                     
                     // populate indexes array, set currentIndex, and replace wrap matched string in a span
                     cellHTML = cellHTML.replace(me.searchRegExp, function(m) {
                        count += 1;
                        if (Ext.Array.indexOf(me.indexes, idx) === -1) {
                            me.indexes.push(idx);
                        }
                        if (me.currentIndex === null) {
                            me.currentIndex = idx;
                        }
                        return '<span class="' + me.matchCls + '">' + m + '</span>';
                     });
                     // 恢复保护标签
                     Ext.each(matches, function(match) {
                        cellHTML = cellHTML.replace(me.tagsProtect, match); 
                     });
                     // update cell html
                     cell.dom.innerHTML = cellHTML;
                     td = td.next();
                 }
             }, me);

             // results found
             if (me.currentIndex !== null) {
                 me.getSelectionModel().select(me.currentIndex);
                 me.statusBar.setStatus({
                     text: count + ' 行匹配.',
                     iconCls: 'x-status-valid'
                 });
             }
         }

         // no results found
         if (me.currentIndex === null) {
             me.getSelectionModel().deselectAll();
         }

         // force textfield focus
         me.textField.focus();
     },
     
    /**
     * Finds all strings that matches thesearched value in each grid cells.
     * 在每个单元格查找所有字符串匹配搜索值
     * 和onTextFieldChange()一样,实现的功能不同
     * @private
     */
    onSearch: function() {
        var me = this,
             count = 0;
        /* 取得搜索字符 */
        me.searchValue = me.getSearchValue(); 
        if (!me.searchValue) {
            me.viewConfig={
                getRowClass:function(record,index,rowParams,store){
                    return "";
                }
            };
            me.store.clearFilter();
            me.setMyRowClass=false;
            return;
        }
        me.indexes = [];
        me.currentIndex = null;
        
        //me.searchRegExp = new RegExp(me.searchValue, 'g' + (me.caseSensitive ? '' : 'i'));
        var myStore=me.store;
        me.searchValue = String(me.searchValue).trim().split(",");
        
        myStore.filterBy(function (record, id) {
            var data = record.data;
            for (var p in data) {
                var porp = String(data[p]);
                for (var i = 0; i < me.searchValue.length; i++) {
                    var macther = me.searchValue[i];
                    var macther2 = '^' + Ext.escapeRe(macther);
                    var mathcer = new RegExp(macther2, 'g' + (me.caseSensitive ? '' : 'i'));
                    if (mathcer.test(porp)) {
                        count += 1;
                        me.setMyRowClass=true;
                        if (Ext.Array.indexOf(me.indexes, id) === -1) {
                            me.indexes.push(id);
                        }
                        if (me.currentIndex === null) {
                            me.currentIndex = id;
                        }
                        return true;
                    }
                }
            }
            return false;
        });
        me.statusBar.setStatus({       //这个是用来显示搜索结果数目的框，不必理会
             text: me.defaultStatusText,
             iconCls: ''
        });

    },
    
    /**
     * 选择含有匹配的前一行
     * @private
     */   
    onPreviousClick: function() {
        var me = this,
            idx;
            
        if ((idx = Ext.Array.indexOf(me.indexes, me.currentIndex)) !== -1) {
            me.currentIndex = me.indexes[idx - 1] || me.indexes[me.indexes.length - 1];
            me.getSelectionModel().select(me.currentIndex);
         }
    },
    
    /**
     * 选择含有匹配的下一行
     * @private
     */    
    onNextClick: function() {
         var me = this,
             idx;
             
         if ((idx = Ext.Array.indexOf(me.indexes, me.currentIndex)) !== -1) {
            me.currentIndex = me.indexes[idx + 1] || me.indexes[0];
            me.getSelectionModel().select(me.currentIndex);
         }
    },
    
    /**
     * 切换到区分大小写模式.
     * @private
     */    
    caseSensitiveToggle: function(checkbox, checked) {
        this.caseSensitive = checked;
        //this.onTextFieldChange();
        this.onSearch();
    },
    
    /**
     * 切换到正则表达式模式
     * @private
     */
    regExpToggle: function(checkbox, checked) {
        this.regExpMode = checked;
        //this.onTextFieldChange();
        this.onSearch();
    }
});