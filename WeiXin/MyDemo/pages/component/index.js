// pages/component/index.js
Page({
  data: {
    list: [
      {
        id: "view",
        name: "视图容器",
        open: false,
        pages: ["view", "scroll-view", "swiper"]
      }
    ]
  },
  widgetsToggle: function (e) {
    var id = e.currentTarget.id;
    var dataList = this.data.list;
    for (var i = 0; i < dataList.length; i++) {
      if (dataList[i].id == id) {
        dataList[i].open = !dataList[i].open;
      }
      else {
        dataList[i].open = false;
      }
    }

    this.setData({ list: dataList });
  },

  onLoad: function (options) {
    // 页面初始化 options为页面跳转所带来的参数
    console.log("index onLoad");
  },
  onReady: function () {
    // 页面渲染完成
    console.log("index onReady");
  },
  onShow: function () {
    // 页面显示
    console.log("index onShow");
  },
  onHide: function () {
    // 页面隐藏
    console.log("index onHide");
  },
  onUnload: function () {
    // 页面关闭
    console.log("index onUnload");
  },
  onPullDownRefresh: function () {
    // 用户下拉动作
    console.log("index onPullDownRefresh");
  },
  onReachBottom: function () {
    // 页面上拉触底
    console.log("index onReachBottom");
  },
  onShareAppMessage: function () {
    // 用户点击右上角分享
    console.log("index onShareAppMessage");
  }
})