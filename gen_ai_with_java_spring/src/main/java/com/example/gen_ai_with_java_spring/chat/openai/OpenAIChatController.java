package com.example.gen_ai_with_java_spring.chat.openai;

import org.springframework.ai.chat.client.ChatClient;
import org.springframework.ai.chat.client.ChatClientRequest;
import org.springframework.ai.chat.client.ChatClientResponse;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/openai/chat")
public class OpenAIChatController {

    private final ChatClient chatClient;

    private final String SYSTEM_PROMPT = "You are a helpful assistant that summarize any given content." 
    +" Ensure the summary is concise, informative, and captures the key points.Use a friendly and "
    +" approachable tone while maintaining professionalism.Do not answer anything other than the"
    +" summarization. If the question is not about summarization respond with 'I can Only help with "
    +"summarization tasks";

    public OpenAIChatController(
            @Qualifier("openAIGeneralChatClient") ChatClient chatClient) {
        this.chatClient = chatClient;
    }

    @PostMapping
    public String chat(@RequestBody String meetingNotes) {
        return chatClient
                .prompt()
                .system(SYSTEM_PROMPT)
                .user(u -> u.text("Can you summarize the following meeting notes: {meetingNotes}" +
                                " Use the format as described in the following example while doing the summarization:" +
                                " Input: In today’s sales strategy meeting, we reviewed Q3 targets and performance gaps. The team agreed to focus on enterprise clients and strengthen partnerships." +
                                " A proposal was made to expand into two new regions. Marketing suggested aligning campaigns with sales objectives to improve lead conversion and shorten sales cycles." +
                                " Output:" +
                                " Action Items:" +
                                "* Focus on enterprise clients and partnerships." +
                                "* Explore expansion into two new regions." +
                                "* Align marketing campaigns with sales objectives." +
                                " Decisions:" +
                                "* Enterprise clients prioritized for Q3." +
                                "* Marketing and sales to work jointly on lead conversion.")
                .param("meetingNotes", meetingNotes))
                .call()
                //.chatClientResponse(); 
                .content();
    }
}
