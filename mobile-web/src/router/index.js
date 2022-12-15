import Vue from 'vue'
import VueRouter from 'vue-router'
import TalkView from '../views/TalkView.vue'
import login from '../views/login/login.vue'
import index from '../views/index/index.vue'
import chat from '../views/chat/chat.vue'
import search from '../views/search/search.vue'
import layout from '../layout/layout.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/login',
    component: login,
    name:'login'
  },
  {
    path: '/layout',
    component: layout,
    name:'layout',
    children:[
      {
        path: 'index',
        component: index
      },
      {
        path: 'search',
        component: search
      }
    ]
  },

  {
    path: '/chat',
    component: chat
  }
]

const router = new VueRouter({
  routes
})

export default router
