// pages/API/index/index.js
Page({
  data:{
    menuList:[{
      name: '开放接口',
      APIList: [{
        zhName: '微信登录',
        enName: 'login',
        url: '../login/login'
      }]
    }]
  },
  onLoad:function(options){
    // 页面初始化 options为页面跳转所带来的参数
  },
  onReady:function(){
    // 页面渲染完成
  },
  onShow:function(){
    // 页面显示
  },
  onHide:function(){
    // 页面隐藏
  },
  onUnload:function(){
    // 页面关闭
  }
})