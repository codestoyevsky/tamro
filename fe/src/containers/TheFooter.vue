<template>
  <CFooter :fixed="false">
    <div>
      <a href="https://www.linkedin.com/in/vugar.gasimov" target="_blank">Vugar Gasimov</a>
      <span class="ml-1">&copy; {{new Date().getFullYear()}}</span>
    </div>
  </CFooter>
</template>

<script>
export default {
  name: 'TheFooter'
}
</script>
