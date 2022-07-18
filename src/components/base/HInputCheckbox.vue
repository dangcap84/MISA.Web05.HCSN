<template>
    <label class="u-checkbox">
      <input type="checkbox" name="" :id="inputId" :value="inputId" @change="checkboxChanged($event)" v-model="model" :checked="isChecked">
      <div class="u-checkbox-icon-wrapper">
        <span class="u-checkbox-icon u-checkbox-unchecked" v-if="!isChecked">
          <img src="@/assets/icons/checkbox-unchecked.svg" alt="">
        </span>
        <span class="u-checkbox-icon u-checkbox-checked" v-if="isChecked">
          <img src="@/assets/icons/checkbox-checked.svg" alt="">
        </span>
      </div>
      <span class="u-label" v-if="label != ''">{{ label }}</span>
    </label>
</template>

<script>

export default {
  name: 'InputCheckbox',

  props: {
    label: {
      type: String,
      default: ''
    },
    inputId: String,
    isChecked: Boolean,
    value: Array,
    index: {
      type: Number,
      default: 0
    }
  },

  data() {
    return {
    }
  },

  computed: {
    model: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    }
  },

  methods: {
    checkboxChanged(event) {
      this.$emit('change', this.$props.index, event.target.checked);
    }
  }
}
</script>

<style scoped>
label {
  margin-bottom: 0;
}
.u-checkbox {
  width: 100%;
  min-width: 14px;
  height: 100%;
  display: flex;
  align-items: center;
}

.u-checkbox:hover {
  cursor: pointer;
}

input[type=checkbox] {
  display: none;
}

.u-checkbox-icon-wrapper {
  position: relative;
  height: 14px;
  width: 14px;
  display: inline-block;
}

.u-checkbox-icon-wrapper > .u-checkbox-icon {
  width: 14px;
  height: 14px;
  display: flex;
  position: absolute;
  left: 0;
  top: 0;
}

.u-checkbox-icon {
  border-radius: 1px;
}

.u-checkbox-icon:hover {
  box-shadow: var(--button-focus-box-shadow);
}

.u-checkbox-icon svg {
  width: 14px;
  height: 14px;
}

label .u-label {
  margin-left: 10px;
  height: 100%;
  overflow: hidden;
}
</style>