LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

LOCAL_CFLAGS += -std=c99
LOCAL_LDLIBS := -lm

LOCAL_MODULE    := duktape
LOCAL_SRC_FILES := duktape.c

include $(BUILD_SHARED_LIBRARY)
