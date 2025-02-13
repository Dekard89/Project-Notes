<template>
  <div class="dialog" v-if="showModal===true" @click.stop="hideModal">
    <div @click.stop class="dialog__content" role="document">
      <slot></slot>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    showModal: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    hideModal() {
      this.$emit('update:showModal',false);
    }
  }
}
</script>
<style scoped>
.dialog {
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  position: fixed;
  display: flex;
  z-index: 100;
}
.dialog__content {
  margin: auto;
  background-color: rgba(192, 192, 192, 0.3);
  border-radius: 12px;
  min-width: 300px;
  min-height: 50px;

}
</style>