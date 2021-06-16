import Vue from 'vue'
import VueI18n from 'vue-i18n'

Vue.use(VueI18n)

const messages = {
  en: {
    menu: {
      dashboard: 'Dashboard',
      users: 'Users',
      singout: 'Sign Out',
    }
  }
}

export default new VueI18n({
  locale: getBrowserLocale(),
  fallbackLocale: 'en',
  messages,
})

function getBrowserLocale() {
  const navigatorLocale =
    navigator.languages !== undefined ?
    navigator.languages[0] :
    navigator.language

  if (!navigatorLocale) {
    return undefined
  }
  
  return navigatorLocale.trim().split(/-|_/)[0]
}